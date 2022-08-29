
import {AppState} from '../state';
import {createReducer, on, State, Action} from '@ngrx/store';
import * as userActions from './user.actions';
import {IUser, User, UserState} from './user.model';
export type UserAction = userActions.All;
const defaultUser = new User(null, 'GUEST');
/**
 * Define all store queries for Post(s)
 */
export namespace UsersQuery {
	export const getUser = (state: AppState) => state.userState;
}
/// Reducer function
export function userReducer(state: UserState = {...defaultUser, loading: true}, action: Action): UserState {
	const userAction = action as UserAction;
	switch (userAction.type) {
		case userActions.GET_USER:
			return {...state, loading: true};
		case userActions.GOOGLE_LOGIN:
			return {...state, loading: true};
		case userActions.AUTHENTICATED:
			return {...state, ...userAction.payload, loading: false};
		case userActions.NOT_AUTHENTICATED:
			return {...state, loading: false};
		case userActions.AUTH_ERROR:
			return {...state, ...userAction.payload, loading: false};
		case userActions.LOGOUT:
			return {...defaultUser, loading: true}
		default:
			return state;
	}
}

