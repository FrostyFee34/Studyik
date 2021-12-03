// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
	apiUrl: 'https://localhost:5001/api/',
	firebase: {
		projectId: 'studyik',
		appId: '1:222319645343:web:24503ff8b628a3171488aa',
		storageBucket: 'studyik.appspot.com',
		apiKey: 'AIzaSyBgnMgciy9lFt9fMQtXURGXn7_jrX8nbHo',
		authDomain: 'studyik.firebaseapp.com',
		messagingSenderId: '222319645343',
		measurementId: 'G-KZ4JHVTQDM',
	},
	production: false,
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
