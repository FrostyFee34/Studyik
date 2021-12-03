import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { HttpClient } from '@angular/common/http';
import firebase from 'firebase/compat/app';
import { IUser, User } from '../shared/models/user';
import { environment } from 'src/environments/environment';

@Injectable({
	providedIn: 'root',
})
export class AuthService {
	baseUrl = environment.apiUrl;
	user$: BehaviorSubject<IUser | null> = new BehaviorSubject<IUser | null>(
		null
	);

	constructor(
		private fireAuth: AngularFireAuth,
		private httpclient: HttpClient, 
	) {
		this.fireAuth.authState.subscribe((firebaseUser) => {
			this.configureAuthState(firebaseUser);
		});
	}

	configureAuthState(firebaseUser: firebase.User | null): void {
		if (firebaseUser) {
			firebaseUser.getIdToken().then(
				(theToken) => {
					console.log('we have a token');
					this.httpclient
						.post(this.baseUrl + 'accounts/verify', { token: theToken })
						.subscribe({
							next: () => {
								let theUser = new User();

								theUser.displayName = firebaseUser.displayName;
								theUser.email = firebaseUser.email;
								theUser.uid = firebaseUser.uid;

								theUser.isSignedIn = true;
								localStorage.setItem('jwt', theToken);
								console.log(theUser);
								this.user$.next(theUser);
							},
							error: (err) => {
								console.log('inside the error from server', err);
								this.logout();
							},
						});
				},
				(failReason) => {
					this.logout();
				}
			);
		} else {
			this.doSignedOutUser();
		}
	}

	doGoogleSignIn(): Promise<void> {
		var googleProvider = new firebase.auth.GoogleAuthProvider();
		googleProvider.addScope('email');
		googleProvider.addScope('profile');
		return this.fireAuth.signInWithPopup(googleProvider).then((auth) => {});
	}

	private doSignedOutUser() {
		let theUser = new User();
		theUser.displayName = null;
		theUser.email = null;
		theUser.uid = null;
		theUser.isSignedIn = false;
		localStorage.removeItem('jwt');
		this.user$.next(theUser);
	}

	signUp(email: string, password: string, name: string) {
		return this.fireAuth
			.createUserWithEmailAndPassword(email, password)
			.then((result) => {
				result.user?.updateProfile({ displayName: name });
			});
	}
	signIn(email: string, password: string) {
		return this.fireAuth.signInWithEmailAndPassword(email, password).then();
	}
	logout(): Promise<void> {
		this.doSignedOutUser();
		return this.fireAuth.signOut();
	}

	getUserObservable(): Observable<User | null> {
		return this.user$.asObservable();
	}

	getToken(): string | null {
		return localStorage.getItem('jwt');
	}
}
