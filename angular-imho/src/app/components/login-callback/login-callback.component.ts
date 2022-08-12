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
		//following codes are needed to capture search query made of auth code before angular router navigates to ['home']. (After this.router.navigate(['hom']), the search query will be lost).
		//Then block can also be used instead.
		//ex: this.authService.loadDiscoveryDocument().then(()=>{this.authService.tryLoginFlow}).then(()=>{this.router.navigate(['home'])})
		var options: any = {customHashFragment: window.location.search};
		this.authService.loadDiscoveryDocument().then(() => {
			this.authService.tryLoginCodeFlow(options);
		});
		this.router.navigate(['home']);
	}
}




