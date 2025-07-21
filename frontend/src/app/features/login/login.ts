import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { AuthHelper } from '../../core/helpers/auth.helper';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  form!: FormGroup;

  loading = false;
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private authHelper: AuthHelper,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  login(): void {
    this.loading = true;

    const data = this.form.value;

    this.authService
      .login(data)
      .subscribe({
        next: (data) => {
          this.authHelper.setUserAuthenticated(data);
          this.router.navigate(['/home']);


          this.loading = false;
        },
        error: (err) => {
          this.errorMessage = 'E-mail ou senha inválidos';
          this.loading = false;
        }
      });
  }
}
