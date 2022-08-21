//import {authConfig} from '../auth.config';
import {Component, OnInit} from '@angular/core';
import {OAuthErrorEvent, OAuthService} from 'angular-oauth2-oidc';
import {authCodeFlowConfig} from '../../imho.config';
import {ActivatedRoute} from '@angular/router';
import {BehaviorSubject, filter} from 'rxjs';
import {AuthService} from 'src/app/services/auth.service';

@Component({
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
	//public hasLoggedIn: boolean = false;
	constructor(private authService: AuthService) {}
	ngOnInit(): void {
		//if (this.authService.hasValidIdToken) {
		//this.hasLoggedIn = true;
		//} else {
		//this.hasLoggedIn = false;
		//}
	}
	login(): void {
		this.authService.loginCode();
	}
	logout(): void {
		this.authService.logout();
		//this.hasLoggedIn = false;
	}
	get hasLoggedIn(): boolean {
		return this.authService.hasValidIdToken;
	}
}
