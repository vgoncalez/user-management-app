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
  isEdit = false;
  userId?: string;

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

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id')!;
    this.isEdit = !!this.userId;

    // if (this.isEdit) {
    //   this.userService.getById(this.userId).subscribe(user => {
    //     this.userForm.patchValue({
    //       nome: user.nome,
    //       email: user.email,
    //       senha: '', // deixa vazio, só preenche se for atualizar
    //     });
    //   });
    // }
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.userForm.invalid) {
      return;
    }

    if (this.isEdit) {
      this.userService
        .updateUser(this.userId!, this.userForm.value)
        .subscribe(() => {
          alert('Usuário atualizado com sucesso!');
          this.router.navigate(['/users']);
        });
    } else {
      this.userService
        .createUser(this.userForm.value)
        .subscribe(() => {
          alert('Usuário criado com sucesso!');
          this.router.navigate(['/users']);
        });
    }
  }
}
