import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {OAuthService} from 'angular-oauth2-oidc';

@Component({
	selector: 'app-logout-callback',
	templateUrl: './logout-callback.component.html',
	styleUrls: ['./logout-callback.component.css']
})
export class LogoutCallbackComponent implements OnInit {

	constructor(private readonly authService: OAuthService, private readonly router: Router) {}

	ngOnInit(): void {
		//this.authService.completeLogout();
		this.router.navigate(['home']);
	}

}
