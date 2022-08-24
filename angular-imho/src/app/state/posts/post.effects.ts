
import {Injectable} from '@angular/core';
import {Effect, Actions, ofType} from '@ngrx/effects';
import {Store} from '@ngrx/store'
import {Observable, of, from, switchMap, map, catchError, defer, tap} from 'rxjs';
import {AppState} from '../state';
import {Image, Post} from './post.model';
import {User} from '../users';
import {PostQuery} from './post.reducer';
import * as postActions from './post.actions';
import {OAuthService} from 'angular-oauth2-oidc';
import {AuthService} from 'src/app/services/auth.service';
import {PostService} from 'src/app/services/post.service';
import {UsersQuery} from '../users';
import {PostActions} from '.';
import {ImageService} from 'src/app/services/image.service';
type Action = postActions.All;
@Injectable()
export class PostEffects {
	posts$ = this.store.select(PostQuery.getPostState);
	user$ = this.store.select(UsersQuery.getUser);
	@Effect({dispatch: false})
	makePost$: Observable<any> = this.actions$.pipe(ofType(postActions.MAKE_POST)
		, map((action: postActions.MakePost) => {
			const post: Post = action.payload;
			//console.log(post);
			if (post === null) {
				throw new Error("Post to be made cannot be null!");
			} else {
				this.postService.makePost(post).subscribe((res) => {
					const newPost: Post = {body: res.body, title: res.title, postid: res['postId'], channelId: res.channelId, tagId: res.tagId, images: []};
					this.imageService.getImageFiles([...(res.images)]).subscribe((img: Image) => {
						//console.log(img);
						newPost.images.push(img);
						this.store.dispatch(new postActions.PostMade(newPost));
					});
				});
			}
		}));
	@Effect({dispatch: false}) getPosts$: Observable<any> = this.actions$.pipe(ofType(postActions.GET_POSTS)
		, map((action: postActions.GetPosts) => {
			this.postService.getPosts().subscribe((postsLoaded) => {
				this.store.dispatch(new PostActions.PostLoaded(postsLoaded));
			})
		})
		, catchError((err: any) => of(new postActions.PostError({error: err.message}))));


	@Effect({dispatch: false}) editPost$: Observable<any> = this.actions$.pipe(ofType(postActions.EDIT_POST)
		, map((action: postActions.EditPost) => {
			this.postService.updatePost(action.payload).subscribe((postEdited) => {
				this.store.dispatch(new PostActions.PostEdited(postEdited));
			})
		})
		, catchError((err: any) => of(new postActions.PostError({error: err.message}))));


	@Effect({dispatch: false}) deletePost$: Observable<any> = this.actions$.pipe(ofType(postActions.DELETE_POST)
		, map((action: postActions.DeletePost) => {
			this.postService.deletePost(action.payload).subscribe((postDeleted) => {
				this.store.dispatch(new PostActions.PostDeleted(postDeleted));
			})
		})
		, catchError((err: any) => of(new postActions.PostError({error: err.message}))));


	@Effect({dispatch: false})
	init$: Observable<any> = defer(() => {
		this.store.dispatch(new postActions.GetPosts());
		return of(123);
	});
	constructor(
		private actions$: Actions,
		private store: Store<AppState>,
		private authService: AuthService,
		private postService: PostService, private imageService: ImageService
	) {}



}
