import {Injectable} from '@angular/core';
import {HttpClient, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, BehaviorSubject, } from 'rxjs';
import {OAuthService} from 'angular-oauth2-oidc';
import {authCodeFlowConfig} from '../imho.config';
import {ActivatedRoute} from '@angular/router';
import {filter} from 'rxjs/operators';
import {Store} from '@ngrx/store'
import {AppState} from '../state/state';
import {UserActions} from '../state/users';
import {User} from 'oidc-client';
import {User as AppUser} from '../state/users'
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
	private isAuthenticatedSubject$ = new BehaviorSubject<boolean>(false);
	public isAuthenticated$ = this.isAuthenticatedSubject$.asObservable();
	private isDoneLoadingSubject$ = new BehaviorSubject<boolean>(false);
	public isDoneLoading$ = this.isDoneLoadingSubject$.asObservable();
	private authServerUrl = "https://localhost:7089";
	private headers: HttpHeaders;
	private httpOptions: {headers?: HttpHeaders};
	loginFailed: boolean = false;
	userProfile?: object;
	usePopup: boolean = false;
	login: boolean = false;
	constructor(
		private route: ActivatedRoute,
		private oauthService: OAuthService, private store: Store<AppState>, private http: HttpClient
	) {
		this.oauthService.events
			.pipe(filter((e) => e.type === 'token_received'))
			.subscribe((_) => {
				//this.store.dispatch(new UserActions.OidcAuthenticated(this.id_token));
				this.getUserData(this.id_token).subscribe((user: AppUser) => {
					console.log("AppUser received from the auth server");
					console.log(user);
					this.store.dispatch(new UserActions.Authenticated(user));
				});
			});
	}
	getUserData(idToken: string): any {
		const claims = this.oauthService.getIdentityClaims();
		const userDataUrl = `${this.authServerUrl}/oidc-api/user-profile`
		console.log(userDataUrl);
		return this.http.get<AppUser>(userDataUrl, this.httpOptions);
	}
	get hasValidAccessToken() {
		return this.oauthService.hasValidAccessToken();
	}
	get hasValidIdToken() {
		return this.oauthService.hasValidIdToken();
	}
	ngOnInit() {
		this.route.params.subscribe((p) => {
			this.login = p['login'];
		});
	}
	async loginCode() {
		if (this.hasValidIdToken) {
			console.log("Already has valid id token");
			return;
		} else {
			this.oauthService.configure(authCodeFlowConfig);
			await this.oauthService.loadDiscoveryDocument();
			sessionStorage.setItem('flow', 'code');
			this.oauthService.initCodeFlow();
		}
	}
	logout() {
		this.oauthService.revokeTokenAndLogout();
	}
	loadUserProfile(): void {
		this.oauthService.loadUserProfile().then((up) => (this.userProfile = up));
	}
	startAutomaticRefresh(): void {
		this.oauthService.setupAutomaticSilentRefresh();
	}

	stopAutomaticRefresh(): void {
		this.oauthService.stopAutomaticRefresh();
	}
	get userId() {
		var claims = this.oauthService.getIdentityClaims();
		if (!claims) return null;
		console.log(claims);
		return claims['sub'];
	}

	get givenName() {
		var claims = this.oauthService.getIdentityClaims();
		if (!claims) return null;
		return claims['given_name'];
	}

	get roles() {
		var claims = this.oauthService.getIdentityClaims();
		if (!claims) return null;
		return claims['roles'];
	}

	get familyName() {
		var claims = this.oauthService.getIdentityClaims();
		if (!claims) return null;
		return claims['family_name'];
	}
	refresh() {
		this.oauthService.oidc = true;

		if (
			!this.oauthService.useSilentRefresh &&
			this.oauthService.responseType === 'code'
		) {
			this.oauthService
				.refreshToken()
				.then((info) => console.debug('refresh ok', info))
				.catch((err) => console.error('refresh error', err));
		} else {
			this.oauthService
				.silentRefresh()
				.then((info) => console.debug('silent refresh ok', info))
				.catch((err) => console.error('silent refresh error', err));
		}
	}

	set requestAccessToken(value: boolean) {
		this.oauthService.requestAccessToken = value;
		localStorage.setItem('requestAccessToken', '' + value);
	}

	get requestAccessToken() {
		return this.oauthService.requestAccessToken ?? false;
	}

	set useHashLocationStrategy(value: boolean) {
		const oldValue = localStorage.getItem('useHashLocationStrategy') === 'true';
		if (value !== oldValue) {
			localStorage.setItem('useHashLocationStrategy', value ? 'true' : 'false');
			window.location.reload();
		}
	}

	get useHashLocationStrategy() {
		return localStorage.getItem('useHashLocationStrategy') === 'true';
	}

	get id_token() {
		return this.oauthService.getIdToken();
	}

	get access_token() {
		return this.oauthService.getAccessToken();
	}
	//get token() {
	//let claims: any = this.oauthService.getIdentityClaims();
	//return claims ? claims : null;
	//}

	get id_token_expiration() {
		return this.oauthService.getIdTokenExpiration();
	}

	get access_token_expiration() {
		return this.oauthService.getAccessTokenExpiration();
	}
}

