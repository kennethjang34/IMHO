import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {OAuthService} from 'angular-oauth2-oidc';
@Injectable(


)
export class JwtInterceptor implements HttpInterceptor {
	constructor(private oauthService: OAuthService) {}
	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		const token = sessionStorage['id_token']; // you probably want to store it in localStorage or something
		if (!token || !this.oauthService.hasValidIdToken()) {
			return next.handle(req);
		}
		const req1 = req.clone({
			headers: req.headers.set('Authorization', `Bearer ${token}`),
		});
		return next.handle(req1);
	}
}
@Injectable({
	providedIn: 'root'
})
export class AuthService {
	constructor(private oauthService: OAuthService) {}
}
