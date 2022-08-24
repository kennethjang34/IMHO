import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {StoreModule} from '@ngrx/store';
import {userReducer} from './users/user.reducer';
import {postReducer} from './posts/post.reducer';
import {UserEffects} from './users';
import {EffectsModule} from '@ngrx/effects';
import {StoreDevtoolsModule} from '@ngrx/store-devtools';
import {PostEffects} from './posts';
@NgModule({
	declarations: [],
	imports: [
		CommonModule,
		EffectsModule.forRoot([
			UserEffects,
			PostEffects
		]),
		StoreModule.forRoot({userState: userReducer, postState: postReducer}),
		StoreDevtoolsModule.instrument({maxAge: 25}),
	],
	providers: [UserEffects,],
	exports: [StoreModule]
})
export class AppStateModule {}
