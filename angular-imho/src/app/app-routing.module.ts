import {NgModule, Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, RouterModule, RouterStateSnapshot, Routes, UrlTree} from '@angular/router';
import {OAuthModule} from 'angular-oauth2-oidc';
import {Observable} from 'rxjs';
import {FeedHomeComponent} from './components/feed-home/feed-home.component';
import {HomeComponent} from './components/home/home.component';
import {LoginCallbackComponent} from './components/login-callback/login-callback.component';
import {LogoutCallbackComponent} from './components/logout-callback/logout-callback.component';
import {SilentCallbackComponent} from './components/silent-callback/silent-callback.component';
import {OktaCallbackComponent} from '@okta/okta-angular';


const routes: Routes = [
	{path: 'home', component: HomeComponent}, {path: 'auth', component: LoginCallbackComponent}, {path: 'google-signout', component: LogoutCallbackComponent}, {path: '**', redirectTo: 'home'}
];

@NgModule({
	imports: [RouterModule.forRoot(routes,), OAuthModule.forRoot({
		resourceServer: {
			allowedUrls: ['https://localhost:7089'],
			sendAccessToken: false,
		}
	})],
	exports: [RouterModule]
})
export class AppRoutingModule {}
