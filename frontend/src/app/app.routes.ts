import { Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { MainLayout } from './core/layouts/main-layout/main-layout';
import { UnauthGuard } from './core/guards/unauth.guard';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'home',
      },
      {
        path: 'home',
        loadComponent: () => import('./features/home/home').then(m => m.Home)
      },
      {
        path: 'users',
        loadChildren: () => import('./features/users/users.routes').then(m => m.USER_ROUTES)
      }
    ]
  },
  {
    path: 'login',
    canActivate: [UnauthGuard],
    loadComponent: () => import('./features/login/login').then(m => m.Login)
  },
  {
    path: '**',
    redirectTo: 'home',
  }
];
