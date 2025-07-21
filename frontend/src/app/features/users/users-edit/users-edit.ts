import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UserService } from '../services/user.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-users-edit',
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './users-edit.html',
  styleUrl: './users-edit.scss'
})
export class UsersEdit {
  userForm: FormGroup;
  submitted = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService
  ) {
    this.userForm = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  get f() {
    return this.userForm.controls;
  }

  ngOnInit(): void { }

  onSubmit(): void {
    this.submitted = true;

    if (this.userForm.invalid) {
      return;
    }

    this.userService
      .createUser(this.userForm.value)
      .subscribe(() => {
        alert('Usuário criado com sucesso!');
        this.router.navigate(['/users']);
      });
  }
}
