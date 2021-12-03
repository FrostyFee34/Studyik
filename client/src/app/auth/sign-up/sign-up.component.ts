import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {MaterialsService} from 'src/app/materials/materials.service';
import {AuthService} from '../auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
})
export class SignUpComponent implements OnInit {
  registerForm!: FormGroup;
  returnUrl!: string;
  errors?: string[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService,
    private homeService: MaterialsService
  ) {
  }

  ngOnInit(): void {
    this.createRegisterForm();
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '';
  }

  createRegisterForm() {
    this.registerForm = new FormGroup({
      displayName: new FormControl('', [Validators.required]),
      email: new FormControl('', [
        Validators.required,
        Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
      ]),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    this.authService
      .signUp(
        this.registerForm.value.email,
        this.registerForm.value.password,
        this.registerForm.value.displayName
      )
      .then(
        () => {
          this.router.navigateByUrl(this.returnUrl);
          this.authService.getUserObservable().subscribe(() => {
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
        this.authService.getUserObservable().subscribe(() => {
          this.homeService.updateMaterials();
        });
      },
      (error) => {
        this.toastr.error(error['code']);
      }
    );
  }
}
