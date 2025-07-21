import { Routes } from '@angular/router';

export const USER_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('../users/users-list/users-list').then(m => m.UsersList)
  },
  {
    path: 'new',
    loadComponent: () => import('../users/users-edit/users-edit').then(m => m.UsersEdit)
  },
  {
    path: 'edit/:id',
    loadComponent: () => import('../users/users-edit/users-edit').then(m => m.UsersEdit)
  }
];