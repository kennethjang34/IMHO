import {AuthConfig} from 'angular-oauth2-oidc';

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

