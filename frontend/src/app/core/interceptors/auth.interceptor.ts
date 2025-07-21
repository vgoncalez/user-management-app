import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthHelper } from '../helpers/auth.helper';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authHelper = inject(AuthHelper);
  const token = authHelper.getToken();

  if (token) {
    const cloned = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });

    return next(cloned);
  }

  return next(req);
};
