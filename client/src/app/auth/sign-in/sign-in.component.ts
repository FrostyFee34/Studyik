import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { HomeService } from 'src/app/home/home.service';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
	selector: 'app-sign-in',
	templateUrl: './sign-in.component.html',
	styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent implements OnInit {
	loginForm!: FormGroup;
	returnUrl!: string;

	constructor(
		private authService: AuthService,
		private router: Router,
		private activatedRoute: ActivatedRoute,
		private toastr: ToastrService,
		private homeService: HomeService
	) {}

	ngOnInit(): void {
		this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '';
		this.createLoginForm();
	}

	createLoginForm() {
		this.loginForm = new FormGroup({
			email: new FormControl('', [
				Validators.required,
				Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
			]),
			password: new FormControl('', Validators.required),
		});
	}

	onSubmit() {
		this.authService
			.signIn(this.loginForm.value.email, this.loginForm.value.password)
			.then(
				() => {
					this.router.navigateByUrl(this.returnUrl);
					this.authService.getUserObservable().subscribe((response) => {
						this.homeService.updateMaterials();
					});
				},
				(error) => {
					this.toastr.error(error['code']);
				}
			);
	}

	googleSignIn() {
		this.authService.doGoogleSignIn().then(
			() => {
				this.router.navigateByUrl(this.returnUrl);
				this.authService.getUserObservable().subscribe((response) => {
					this.homeService.updateMaterials();
				});
			},
			(error) => {
				this.toastr.error(error['code']);
			}
		);
	}
}
