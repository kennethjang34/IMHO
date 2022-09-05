

import {Injectable} from '@angular/core';
import {Effect, Actions, ofType} from '@ngrx/effects';
import {Store} from '@ngrx/store'
import {Observable, of, from, switchMap, map, catchError, defer, tap} from 'rxjs';
import {AppState} from '../state';
import {Channel, ChannelState} from './channel.model';
import {User} from '../users';
import {ChannelQuery} from './channel.reducer';
import * as channelActions from './channel.actions';
import {OAuthService} from 'angular-oauth2-oidc';
import {AuthService} from 'src/app/services/auth.service';
import {PostService} from 'src/app/services/post.service';
import {UsersQuery} from '../users';
import {PostActions} from '.';
import {ImageService} from 'src/app/services/image.service';
type Action = channelActions.All;
@Injectable()
export class ChannelEffects {
	channels$ = this.store.select(ChannelQuery.getChannelState);
	user$ = this.store.select(UsersQuery.getUser);
	@Effect({dispatch: false}) getChannels$: Observable<any> = this.actions$.pipe(ofType(channelActions.GET_CHANNELS)
		, map((action: channelActions.GetChannels) => {
			this.store.dispatch(new channelActions.ResetPosts());
			this.postService.getPosts().subscribe((channelLoaded) => {
				this.store.dispatch(new channelActions.ChannelLoaded(channelLoaded));
			})
		})
		, catchError((err: any) => of(new channelActions.ChannelError({error: err.message}))));

	//@Effect({dispatch: false}) editPost$: Observable<any> = this.actions$.pipe(ofType(channelActions.EDIT_POST)
	//, map((action: channelActions.EditPost) => {
	//this.postService.updatePost(action.payload).subscribe((postEdited) => {
	//this.store.dispatch(new PostActions.PostEdited(postEdited));
	//})
	//})
	//, catchError((err: any) => of(new channelActions.PostError({error: err.message}))));
	//@Effect({dispatch: false}) deletePost$: Observable<any> = this.actions$.pipe(ofType(channelActions.DELETE_POST)
	//, map((action: channelActions.DeletePost) => {
	//this.postService.deletePost(action.payload).subscribe((postDeleted) => {
	//this.store.dispatch(new PostActions.PostDeleted(postDeleted));
	//})
	//})
	//, catchError((err: any) => of(new channelActions.PostError({error: err.message}))));

	constructor(
		private actions$: Actions,
		private store: Store<AppState>,
		private authService: AuthService,
		private postService: PostService, private imageService: ImageService
	) {}



}
