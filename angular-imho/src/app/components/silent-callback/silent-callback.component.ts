import {Component, OnInit} from '@angular/core';
import {OAuthService} from 'angular-oauth2-oidc';
@Component({
	selector: 'app-silent-callback',
	templateUrl: './silent-callback.component.html',
	styleUrls: ['./silent-callback.component.css']
})
export class SilentCallbackComponent implements OnInit {

	constructor(private readonly authService: OAuthService) {}
	ngOnInit(): void {
		//this.authService.silentSignInAuthentication();
	}

}
