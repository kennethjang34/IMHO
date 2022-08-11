import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {JwksValidationHandler, OAuthService} from 'angular-oauth2-oidc';
import {authCodeFlowConfig} from 'src/app/imho.config';
@Component({
	selector: 'app-login-callback',
	templateUrl: './login-callback.component.html',
	styleUrls: ['./login-callback.component.css']
})
export class LoginCallbackComponent implements OnInit {
	private access_token?: any;
	constructor(private readonly authService: OAuthService, private readonly router: Router) {}
	ngOnInit(): void {
		//this.authService.completeAuthentication();
		//this.authService..then(token => {
		//console.log('Token in component:', token);
		//});
		//this.access_token = this.authService.getAccessToken();

		this.authService.configure(authCodeFlowConfig);
		this.authService.oidc = true;
		this.authService.setStorage(sessionStorage);
		this.authService.tokenValidationHandler = new JwksValidationHandler();
		this.authService.loadDiscoveryDocument().then(() => {
			this.authService.tryLoginCodeFlow({
				//onTokenReceived: context => {
				//console.log("logged in");
				//console.log(context);
				//console.log(this.access_token);
				//this.router.navigate(['home']);
				//}
			})
		}).then(
			() => {this.router.navigate(['home'])}
		);
	}

}




//import {Component, OnInit} from '@angular/core';
//import {Router} from '@angular/router';
//import {OAuthService} from 'angular-oauth2-oidc';
//import {authCodeFlowConfig} from 'src/app/imho.config';
//import {JwksValidationHandler} from 'angular-oauth2-oidc';
//import {filter} from 'rxjs';
//@Component({
	//selector: 'app-login-callback',
	//templateUrl: './login-callback.component.html',
	//styleUrls: ['./login-callback.component.css']
//})
//export class LoginCallbackComponent implements OnInit {
	//private access_token?: any;
	//constructor(private router: Router, private oauthService: OAuthService) {
		//// Remember the selected configuration
		//this.configureCodeFlow();
		//// Automatically load user profile
		//this.oauthService.events
			//.pipe(filter((e) => e.type === 'token_received'))
			//.subscribe((_) => {
				//console.debug('state', this.oauthService.state);
				//this.oauthService.loadUserProfile();
				//const scopes = this.oauthService.getGrantedScopes();
				//console.debug('scopes', scopes);
			//});
	//}
	//ngOnInit(): void {
		//this.router.navigate(['home']);
	//}
	//private configureCodeFlow() {
		//this.oauthService.configure(authCodeFlowConfig);
		//this.oauthService.oidc = true;
		//this.oauthService.setStorage(sessionStorage);
		//this.oauthService.tokenValidationHandler = new JwksValidationHandler();
		//this.oauthService.loadDiscoveryDocument().then(() => this.oauthService.tryLoginCodeFlow());
		////this.oauthService.tryLoginCodeFlow();
		////this.oauthService.loadDiscoveryDocumentAndTryLogin();
		////this.oauthService.loadDiscoveryDocumentAndLogin();
		////this.oauthService.setupAutomaticSilentRefresh();
	//}

//}
