import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest
} from '@angular/common/http';

import { TokenService } from '..';
import { debug } from 'util';

/** Pass untouched request through to the next request handler. */
@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private tokenService: TokenService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    // Get the auth token from the service.
    const token = this.tokenService.getToken();

    // Clone the request and replace the original headers with
    // cloned headers, updated with the authorization.
    if (token != null) {
      const authReq = req.clone({
        headers: req.headers.set('Authorization', token)
      });
      return next.handle(authReq);
    }

    // send cloned request with header to the next handler.
    return next.handle(req);
  }
}
