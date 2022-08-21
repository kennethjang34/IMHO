import {AuthConfig} from 'angular-oauth2-oidc';

import {authCodeFlowConfig} from './imho.config';
export const authCodeFlowConfig: AuthConfig = {
	// Url of the Identity Provider
	issuer: 'https://accounts.google.com',
	//
	//     // URL of the SPA to redirect the user to after login
	redirectUri: window.location.origin + '/auth',
	postLogoutRedirectUri: window.location.origin + '/google-signout',
	//         // URL of the SPA to redirect the user after silent refresh
	silentRefreshRedirectUri: window.location.origin + '/silent-refresh.html',
	//
	//             // The SPA's id. The SPA is registerd with this id at the auth-server
	clientId:
		'467353587951-sv7r49l22iq10mscaupqdcnservlbmgv.apps.googleusercontent.com',
	//
	dummyClientSecret: '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',
	strictDiscoveryDocumentValidation: false,
	//
	//                       // set the scope for the permissions the client should request
	//                         // The first three are defined by OIDC. The 4th is a usecase-specific one
	scope: 'openid profile email',
	//https://www.googleapis.com/auth/calendar',
	responseType: 'code',
	//
	showDebugInformation: true,
	//
	sessionChecksEnabled: true,
	//                               
};




//import {AuthConfig, JwksValidationHandler} from 'angular-oauth2-oidc';

//export const authCodeFlowConfig: AuthConfig = {
	//// Url of the Identity Provider
	//issuer: 'https://dev-52978948.okta.com/oauth2/default',
	//// URL of the SPA to redirect the user to after login
	////redirectUri: window.location.origin + '/home.html',

	//// The SPA's id. The SPA is registerd with this id at the auth-server
	//// clientId: 'server.code',
	//clientId: '0oa5w45jmoXwz0buu5d7',
	//// Just needed if your auth server demands a secret. In general, this
	//// is a sign that the auth server is not configured with SPAs in mind
	//// and it might not enforce further best practices vital for security
	//// such applications.
	//dummyClientSecret: '77Bk6MI8azP7e_zMkhGoWZu9tcDUmyL31XhhRzjP',
	//redirectUri: 'http://localhost:4200/okta-auth',
	//postLogoutRedirectUri: 'http://localhost:4200/okta-signout',
	//responseType: 'code',
	//scope: 'openid profile email offline_access',
	//useSilentRefresh: false,
	////silentRefreshRedirectUri: `${window.location.origin}/silent-refresh.html`,
	//sessionChecksEnabled: false,

	////useSilentRefresh: useSilentRefreshForCodeFlow,
	//// set the scope for the permissions the client should request
	//// The first four are defined by OIDC.
	//// Important: Request offline_access to get a refresh token
	//// The api scope is a usecase specific one
	////scope: 'openid profile email offline_access',
	//showDebugInformation: true,
	//clearHashAfterLogin: true
//};
