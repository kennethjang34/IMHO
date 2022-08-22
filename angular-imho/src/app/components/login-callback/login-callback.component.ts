import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {JwksValidationHandler, OAuthService} from 'angular-oauth2-oidc';
import {authCodeFlowConfig} from 'src/app/imho.config';
import {AppState} from 'src/app/state/state';
import {Store} from '@ngrx/store';
import {UserActions} from '../../state/users';
@Component({
	selector: 'app-login-callback',
	templateUrl: './login-callback.component.html',
	styleUrls: ['./login-callback.component.css']
})
export class LoginCallbackComponent implements OnInit {
	private access_token?: any;
	constructor(private readonly authService: OAuthService, private readonly router: Router, private store: Store<AppState>) {}
	ngOnInit(): void {
		this.authService.configure(authCodeFlowConfig);
		this.authService.oidc = true;
		this.authService.setStorage(sessionStorage);
		this.authService.tokenValidationHandler = new JwksValidationHandler();
		//following codes are needed to capture search query made of auth code before angular router navigates to ['home']. (After this.router.navigate(['home']), the search query will be lost).
		//Then block can also be used instead.
		//ex: this.authService.loadDiscoveryDocument().then(()=>{this.authService.tryLoginFlow}).then(()=>{this.router.navigate(['home'])})
		var options: any = {customHashFragment: window.location.search};
		this.authService.loadDiscoveryDocument().then(() => {
			this.authService.tryLoginCodeFlow(options);
		}).then(
			() => {
				this.authService.loadUserProfile().then((credential) => {
					console.log(`credential from google is: ${credential}`);
				})

				////
				////Credential supposed to contain userId,userName fields
				//this.store.dispatch(new UserActions.GoogleLogin(credential));
			}
		);
		this.router.navigate(['home']);
	}
}




