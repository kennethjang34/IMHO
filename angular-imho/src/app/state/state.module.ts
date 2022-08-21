import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {StoreModule} from '@ngrx/store';
import {userReducer} from './users/user.reducer';
import {postReducer} from './posts/post.reducer';
import {UserEffects} from './users';
import {EffectsModule} from '@ngrx/effects';
@NgModule({
	declarations: [],
	imports: [
		EffectsModule.forRoot([
			UserEffects,
			//PostEffects
		]),
		CommonModule, StoreModule.forRoot({user: userReducer, post: postReducer})
	],
	providers: [UserEffects]
})
export class StateModule {}
