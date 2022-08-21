import {Injectable} from '@angular/core';
import {Effect, Actions, ofType} from '@ngrx/effects';
import {Store} from '@ngrx/store'
import {Observable, of, from, switchMap, map, catchError, defer} from 'rxjs';
import {AppState} from '../state';
import {User} from './user.model';
import {UsersQuery} from './user.reducer';
import * as userActions from './user.actions';
import {OAuthService} from 'angular-oauth2-oidc';
import {AuthService} from 'src/app/services/auth.service';
type Action = userActions.All;
@Injectable()
export class UserEffects {

	// ************************************************
	// Observable Queries available for consumption by views
	// ************************************************

	user$ = this.store.select(UsersQuery.getUser);

	// ************************************************
	// Effects to be registered at the Module level
	// ************************************************

	@Effect()
	getUser$: Observable<Action> = this.actions$.pipe(ofType(userActions.GET_USER)
		, map((action: userActions.GetUser) => {
			const authData = action.payload;
			if (authData) {
				const user = new User(authData.UserId, authData.userName);
				return new userActions.Authenticated(user);
			} else {
				return new userActions.NotAuthenticated();
			}
		}), catchError((err: any) => of(new userActions.AuthError())));
	/**
	 *Google OAuth
	 */
	@Effect() loginGoogle$: Observable<Action> = this.actions$.pipe(ofType(userActions.GOOGLE_LOGIN)
		, map((action: userActions.GoogleLogin) => action.payload)
		, switchMap((payload: any) => {
			return from(this.googleLogin());
		})
		//, map((credential: any) => {
		//// successful login
		//return new userActions.GetUser(credential);
		//})
		, catchError((err: any) => {
			return of(new userActions.AuthError({error: err.message}));
		}));
	@Effect() logout$: Observable<Action> = this.actions$.pipe(ofType(userActions.LOGOUT)
		, switchMap((action: userActions.Logout) => {
			return from(this.logout());
		})
		, map(() => {
			return new userActions.NotAuthenticated();
		})
		, catchError((err: any) => of(new userActions.AuthError({error: err.message}))));

	@Effect({dispatch: false})
	init$: Observable<any> = defer(() => {
		this.store.dispatch(new userActions.GetUser());
		return of(123);
	});

	// ************************************************
	// Internal Code
	// ************************************************

	constructor(
		private actions$: Actions,
		private store: Store<AppState>,
		private authService: AuthService,
	) {}

	/**
	 *
	 */
	login(): Observable<User> {
		this.store.dispatch(new userActions.GoogleLogin());
		return this.user$;
	}

	/**
	 *
	 */
	logout(): Observable<User> {
		this.store.dispatch(new userActions.Logout());
		return this.user$;
	}
	// ******************************************
	// Internal Methods
	// ******************************************


	protected googleLogin(): Promise<any> {
		//const provider = new firebase.auth.GoogleAuthProvider();
		return this.authService.loginCode();
		//return this.afAuth.auth.signInWithPopup(provider);
	}

}
