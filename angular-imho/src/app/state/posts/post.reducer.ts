
import {AppState} from '../state';
import {createReducer, on, State, Action} from '@ngrx/store';
import * as postActions from './post.actions';
import {Post, Image} from './post.model';
import {PostState} from './post.model';
export type PostAction = postActions.All;
export namespace PostQuery {
	export const getPostState = (state: AppState) => state.postState;
}
/// Reducer function
export function postReducer(state: PostState = {posts: [], loading: true, images: []}, action: Action): PostState {
	const postAction = action as PostAction;
	switch (postAction.type) {
		case postActions.GET_POSTS:
			return {...state, loading: true};
		case postActions.MAKE_POST:
			return {...state, loading: true};
		case postActions.EDIT_POST:
			//console.log(postAction.payload);
			return {...state, ...postAction.payload, loading: false};
		case postActions.DELETE_POST:
			return {...state, loading: false};
		case postActions.POST_ERROR:
			return {...state, ...postAction.payload, loading: false};
		case postActions.POST_MADE:
			//console.log({...state, posts: [...state.posts, postAction.payload], loading: false});
			return {...state, posts: [...state.posts, postAction.payload], loading: false};
		case postActions.POST_EDITED:
			return {...state, posts: [...state.posts, postAction.payload], loading: false};
		case postActions.POST_DELETED:
			return {...state, posts: [...state.posts].filter((p) => {p !== postAction.payload.postId}), loading: false}
		case postActions.POSTS_LOADED:
			return {...state, posts: [...state.posts, postAction.payload], loading: false};
		default:
			return state;
	}
}

