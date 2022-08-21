
import {Action} from '@ngrx/store';
import {User} from './user.model';

export const GET_USER = '[Auth] Get user';
export const AUTHENTICATED = '[Auth] Authenticated';
export const NOT_AUTHENTICATED = '[Auth] Not Authenticated';

export const GOOGLE_LOGIN = '[Auth] Google login attempt';
export const LOGOUT = '[Auth] Logout';

export const AUTH_ERROR = '[Auth] Error';



export class GetUser implements Action {
	readonly type = GET_USER;
	constructor(public payload?: any) {}
}
export class Authenticated implements Action {
	readonly type = AUTHENTICATED;
	constructor(public payload?: any) {}
}
export class NotAuthenticated implements Action {
	readonly type = NOT_AUTHENTICATED;
	constructor(public payload?: any) {}
}

export class AuthError implements Action {
	readonly type = AUTH_ERROR;
	constructor(public payload?: any) {}
}


export class GoogleLogin implements Action {
	readonly type = GOOGLE_LOGIN;
	constructor(public payload?: any) {}
}

/// Logout Actions

export class Logout implements Action {
	readonly type = LOGOUT;
	constructor(public payload?: any) {}
}

export type All
	= Logout
	| Authenticated
	| NotAuthenticated
	| GoogleLogin
	| AuthError
	| GetUser;
/// Get User AuthState

//export const GetUser = createAction(GET_USER);

//export const Authenticated = createAction(AUTHENTICATED);

//export const NotAuthenticated = createAction(NOT_AUTHENTICATED);

//export const AuthError = createAction(AUTH_ERROR);

///// Google Login Actions

//export const GoogleLogin = createAction(GOOGLE_LOGIN);

///// Logout Actions

//export const Logout = createAction(LOGOUT);


////export type All
////= GetUser 
////| Authenticated
////| NotAuthenticated
////| GoogleLogin
////| AuthError
////| Logout;
