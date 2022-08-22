//import {authConfig} from '../auth.config';
import {Component, OnInit} from '@angular/core';
import {OAuthErrorEvent, OAuthService} from 'angular-oauth2-oidc';
import {authCodeFlowConfig} from '../../imho.config';
import {ActivatedRoute} from '@angular/router';
import {BehaviorSubject, filter} from 'rxjs';
import {AuthService} from 'src/app/services/auth.service';
import {Store} from '@ngrx/store'
import {UserActions} from 'src/app/state/users';
@Component({
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
	//public hasLoggedIn: boolean = false;
	constructor(private authService: AuthService, private store: Store) {}
	ngOnInit(): void {
	}
	login(): void {
		this.store.dispatch(new UserActions.GoogleLogin());
		//this.authService.loginCode();
	}
	logout(): void {
		this.store.dispatch(new UserActions.Logout());
		//this.authService.logout();
	}
	get hasLoggedIn(): boolean {
		return this.authService.hasValidIdToken;
	}
}
