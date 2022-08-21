import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {FontAwesomeModule, FaIconLibrary} from '@fortawesome/angular-fontawesome';
import {fas} from '@fortawesome/free-solid-svg-icons';
import {far} from '@fortawesome/free-regular-svg-icons';
import {fab} from '@fortawesome/free-brands-svg-icons';
import {SidebarComponent} from './components/sidebar/sidebar.component';
import {FeedHomeComponent} from './components/feed-home/feed-home.component';
import {FeedInputComponent} from './components/feed-input/feed-input.component';
import {FeedPostComponent} from './components/feed-post/feed-post.component';
import {WidgetComponent} from './components/widget/widget.component';
import {FormsModule} from '@angular/forms';
import {LoginCallbackComponent} from './components/login-callback/login-callback.component';
import {LogoutCallbackComponent} from './components/logout-callback/logout-callback.component';
import {SilentCallbackComponent} from './components/silent-callback/silent-callback.component';
import {HomeComponent} from './components/home/home.component';
import {OAuthModule, OAuthService} from 'angular-oauth2-oidc';
import {Router, RouterModule, Routes} from '@angular/router';
import {AppStateModule} from './state/state.module'
import {JwtInterceptor} from './services/auth.service';
@NgModule({
	declarations: [
		AppComponent,
		SidebarComponent,
		FeedHomeComponent,
		FeedInputComponent,
		FeedPostComponent,
		WidgetComponent,
		LoginCallbackComponent,
		LogoutCallbackComponent,
		SilentCallbackComponent,
		HomeComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule, FontAwesomeModule, FormsModule, HttpClientModule, AppStateModule
	],
	providers: [OAuthService, {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}, {provide: "API_ADDRESS", useValue: "https://localhost:7089/"}],
	bootstrap: [AppComponent]
})
export class AppModule {
	constructor(faIconLibrary: FaIconLibrary) {
		faIconLibrary.addIconPacks(fas, far, fab);
	}
}
