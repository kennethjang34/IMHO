

import {Action} from '@ngrx/store';
import {Post} from './post.model';

export const MAKE_POST = '[Post] MAKE_POST';
export const POST_MADE = '[Post] POST_MADE';
export const GET_POSTS = '[Post] GET_POSTS';
export const POSTS_LOADED = '[Post] POSTS_LOADED'
export const EDIT_POST = '[Post] EDIT_POST';
export const POST_EDITED = '[Post] POST_EDITED'
export const DELETE_POST = '[Post] DELETE_POST';
export const POST_DELETED = '[Post] POST_DELETED '
export const POST_ERROR = '[Post] Error';

export const RESET_POSTS = '[Post] RESET_POSTS';

export class ResetPosts implements Action {
	readonly type = RESET_POSTS;
	constructor(public payload?: any) {}
}
export class GetPosts implements Action {
	readonly type = GET_POSTS;
	constructor(public payload?: any) {}
}
export class PostLoaded implements Action {
	readonly type = POSTS_LOADED;
	constructor(public payload?: any) {}
}
export class MakePost implements Action {
	readonly type = MAKE_POST;
	constructor(public payload?: any) {}
}
export class PostMade implements Action {
	readonly type = POST_MADE;
	constructor(public payload?: any) {}
}
export class EditPost implements Action {
	readonly type = EDIT_POST;
	constructor(public payload?: any) {}
}
export class PostEdited implements Action {
	readonly type = POST_EDITED;
	constructor(public payload?: any) {}
}
export class DeletePost implements Action {
	readonly type = DELETE_POST;
	constructor(public payload?: any) {}
}
export class PostDeleted implements Action {
	readonly type = POST_DELETED;
	constructor(public payload?: any) {}
}

export class PostError implements Action {
	readonly type = POST_ERROR;
	constructor(public payload?: any) {}
}
export type All
	= ResetPosts | GetPosts | PostLoaded
	| MakePost | PostMade
	| EditPost | PostEdited
	| DeletePost | PostDeleted
	| PostError;
