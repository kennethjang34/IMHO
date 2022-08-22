import {Component, Injectable} from '@angular/core';
import {JwksValidationHandler, OAuthService} from 'angular-oauth2-oidc';
import {authCodeFlowConfig} from './imho.config';
import {filter} from 'rxjs/operators';
import {Router} from '@angular/router';
import {Store} from '@ngrx/store'
import {UserEffects} from './state/users';
import {AppState} from './state/state';
import {UserActions} from './state/users/';




@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css']
})
export class AppComponent {
	title = 'angular-imho';

	//post$: Observable<Post> = this.postService.post$;
	//user$: Observable<User> = this.userService.user$;
	constructor(private router: Router, private oauthService: OAuthService, private store: Store<AppState>
		//private userService: UserEffects
	) {
		// Remember the selected configuration
		this.configureCodeFlow();
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
