export interface IUser {
	uid: string | null;
	email: string | null;
	displayName: string | null;
	isSignedIn: boolean | null;
}

export class User implements IUser {
	uid!: string | null;
	email!: string | null;
	displayName!: string | null;
	isSignedIn!: boolean | null;

}
