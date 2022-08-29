import {Injectable} from '@angular/core';
import {Effect, Actions, ofType} from '@ngrx/effects';
import {Store} from '@ngrx/store'
import {Observable, of, from, switchMap, map, catchError, defer, tap} from 'rxjs';
import {AppState} from '../state';
import {User} from './user.model';
import {UsersQuery} from './user.reducer';
import * as userActions from './user.actions';
import {OAuthService} from 'angular-oauth2-oidc';
import {AuthService} from 'src/app/services/auth.service';
type Action = userActions.All;
@Injectable()
export class UserEffects {
	user$ = this.store.select(UsersQuery.getUser);
	@Effect()
	getUser$: Observable<any> = this.actions$.pipe(ofType(userActions.GET_USER)
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
	@Effect({dispatch: false}) loginGoogle$: Observable<any> = this.actions$.pipe(ofType(userActions.GOOGLE_LOGIN)
		, map((action: userActions.GoogleLogin) => {
			this.authService.loginCode();
			return action.payload;
		})
		, catchError((err: any) => {
			return of(new userActions.AuthError({error: err.message}));
		}));
	@Effect({dispatch: false}) logout$: Observable<any> = this.actions$.pipe(ofType(userActions.LOGOUT)
		, map((action: userActions.Logout) => {
			this.authService.logout();
			return action.payload;
		})
		, catchError((err: any) => of(new userActions.AuthError({error: err.message}))));

	@Effect({dispatch: false})
	init$: Observable<any> = defer(() => {
		this.store.dispatch(new userActions.GetUser());
		return of(123);
	});

	constructor(
		private actions$: Actions,
		private store: Store<AppState>,
		private authService: AuthService,
	) {}

	protected googleLogin(): Promise<any> {
		//const provider = new firebase.auth.GoogleAuthProvider();
		return this.authService.loginCode();
		//return this.afAuth.auth.signInWithPopup(provider);
	}

}
