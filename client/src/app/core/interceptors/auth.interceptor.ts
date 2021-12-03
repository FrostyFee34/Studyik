import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(public authService: AuthService) {}

  // If user is authorized add a header to the request
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    var token = this.authService.getToken();
    if (token) {
      var header = "Bearer " + token;
      var reqWithAuth = req.clone({ headers: req.headers.set("Authorization", header) });
      return next.handle(reqWithAuth);
    }

    return next.handle(req);
  }

}
