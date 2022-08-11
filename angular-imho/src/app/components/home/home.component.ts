//import {authConfig} from '../auth.config';
import {Component, OnInit} from '@angular/core';
import {OAuthErrorEvent, OAuthService} from 'angular-oauth2-oidc';
import {authCodeFlowConfig} from '../../imho.config';
import {ActivatedRoute} from '@angular/router';
import {BehaviorSubject, filter} from 'rxjs';

@Component({
	templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
	private isAuthenticatedSubject$ = new BehaviorSubject<boolean>(false);
	public isAuthenticated$ = this.isAuthenticatedSubject$.asObservable();

	private isDoneLoadingSubject$ = new BehaviorSubject<boolean>(false);
	public isDoneLoading$ = this.isDoneLoadingSubject$.asObservable();
	loginFailed: boolean = false;
	userProfile?: object;
	usePopup: boolean = false;
	login: boolean = false;
	constructor(
		private route: ActivatedRoute,
		private oauthService: OAuthService
	) {
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
		// Tweak config for code flow
		this.oauthService.configure(authCodeFlowConfig);
		await this.oauthService.loadDiscoveryDocument();
		sessionStorage.setItem('flow', 'code');
		this.oauthService.initCodeFlow();
		this.oauthService.tryLoginCodeFlow();
	}
	logout() {
		// this.oauthService.logOut();
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
