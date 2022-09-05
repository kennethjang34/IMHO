//import {authConfig} from '../auth.config';
import {Component, OnInit} from '@angular/core';
import {OAuthErrorEvent, OAuthService} from 'angular-oauth2-oidc';
import {authCodeFlowConfig} from '../../imho.config';
import {ActivatedRoute} from '@angular/router';
import {BehaviorSubject, filter, Observable} from 'rxjs';
import {AuthService} from 'src/app/services/auth.service';
import {Store} from '@ngrx/store'
import {UserActions} from 'src/app/state/users';
import {Channel, ChannelQuery, ChannelState} from 'src/app/state/channels';
import {AppState} from 'src/app/state/state';
@Component({
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
	//public hasLoggedIn: boolean = false;
	channelSelected: Channel;
	//channels: Channel[];
	channelState$: Observable<ChannelState>;
	constructor(private authService: AuthService, private store: Store<AppState>) {
		this.channelState$ = this.store.select(ChannelQuery.getChannelState)
	}
	ngOnInit(): void {
	}
	login(): void {
		this.store.dispatch(new UserActions.GoogleLogin());
	}
	logout(): void {
		this.store.dispatch(new UserActions.Logout());
	}
	get hasLoggedIn(): boolean {
		return this.authService.hasValidIdToken;
	}

}
