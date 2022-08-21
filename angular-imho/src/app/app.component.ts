import {Component, Injectable} from '@angular/core';
import {JwksValidationHandler, OAuthService} from 'angular-oauth2-oidc';
import {authCodeFlowConfig} from './imho.config';
import {filter} from 'rxjs/operators';
import {Router} from '@angular/router';
//import {}
import {UserEffects} from './state/users';




@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css']
})
export class AppComponent {
	title = 'angular-imho';

	//post$: Observable<Post> = this.postService.post$;
	//user$: Observable<User> = this.userService.user$;
	constructor(private router: Router, private oauthService: OAuthService,
		//private userService: UserEffects
	) {
		// Remember the selected configuration
		this.configureCodeFlow();
		// Automatically load user profile
		this.oauthService.events
			.pipe(filter((e) => e.type === 'token_received'))
			.subscribe((_) => {
				console.debug('state', this.oauthService.state);
				this.oauthService.loadUserProfile();
				const scopes = this.oauthService.getGrantedScopes();
				console.debug('scopes', scopes);
			});
	}
	private configureCodeFlow() {
		this.oauthService.configure(authCodeFlowConfig);
		this.oauthService.oidc = true;
		this.oauthService.setStorage(sessionStorage);
		this.oauthService.tokenValidationHandler = new JwksValidationHandler();
		this.oauthService.loadDiscoveryDocument().then(() => this.oauthService.tryLoginCodeFlow());
		//this.oauthService.tryLoginCodeFlow();
		//this.oauthService.loadDiscoveryDocumentAndTryLogin();
		//this.oauthService.loadDiscoveryDocumentAndLogin();
		//this.oauthService.setupAutomaticSilentRefresh();
	}
}
