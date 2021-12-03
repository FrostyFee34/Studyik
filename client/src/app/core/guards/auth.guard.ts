import { Injectable } from '@angular/core';
import {
	ActivatedRouteSnapshot,
	CanActivate,
	Router,
	RouterStateSnapshot,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { map } from 'rxjs/operators';

@Injectable({
	providedIn: 'root',
})
export class AuthGuard implements CanActivate {
	constructor(private authservice: AuthService, private router: Router) {}

	canActivate(
		next: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): Observable<boolean> {
		return this.authservice.getUserObservable().pipe(
			map((user) => {
				if (user !== null && user.isSignedIn === true) {
					return true;
				}
				return false;
			})
		);
	}
}
