
import {AppState} from '../state';
import {createReducer, on, State, Action} from '@ngrx/store';
import * as userActions from './user.actions';
import {IUser, User} from './user.model';
export type UserAction = userActions.All;
const defaultUser = new User(null, 'GUEST');
/**
 * Define all store queries for Post(s)
 */
export namespace UsersQuery {
	export const getUser = (state: AppState) => state.user;
}
export interface UserState {
	loading: boolean,
	user?: IUser,
}

/// Reducer function
export function userReducer(state: UserState = {user: defaultUser, loading: true}, action: Action): UserState {
	const userAction = action as UserAction;
	switch (userAction.type) {
		case userActions.GET_USER:
			return {...state, loading: true};
		case userActions.GOOGLE_LOGIN:
			return {...state, loading: true};
		case userActions.AUTHENTICATED:
			return {...state, ...userAction.payload, loading: false};
		case userActions.NOT_AUTHENTICATED:
			return {...state, ...defaultUser, loading: false};
		case userActions.AUTH_ERROR:
			return {...state, ...userAction.payload, loading: false};
		case userActions.LOGOUT:
			return {...state, loading: true};
		default:
			return state;
	}
}

