import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {FontAwesomeModule, FaIconLibrary} from '@fortawesome/angular-fontawesome';
import {fas} from '@fortawesome/free-solid-svg-icons';
import {far} from '@fortawesome/free-regular-svg-icons';
import {fab} from '@fortawesome/free-brands-svg-icons';
import {SidebarComponent} from './components/sidebar/sidebar.component';
import {FeedHomeComponent} from './components/feed-home/feed-home.component';
import {FeedInputComponent} from './components/feed-input/feed-input.component';
import { FeedPostComponent } from './components/feed-post/feed-post.component';
import { WidgetComponent } from './components/widget/widget.component';
@NgModule({
	declarations: [
		AppComponent,
		SidebarComponent,
		FeedHomeComponent,
		FeedInputComponent,
  FeedPostComponent,
  WidgetComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule, FontAwesomeModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule {
	constructor(faIconLibrary: FaIconLibrary) {
		faIconLibrary.addIconPacks(fas, far, fab);
	}
}
