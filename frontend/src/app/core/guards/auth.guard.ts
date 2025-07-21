import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthHelper } from '../helpers/auth.helper';

export const AuthGuard: CanActivateFn = () => {
  const authHelper = inject(AuthHelper);
  const router = inject(Router);
  return authHelper.isAuthenticated() ? true : router.createUrlTree(['/login']);
};
