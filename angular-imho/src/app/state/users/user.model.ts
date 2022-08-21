export interface IUser {
	userId: string;
	userName: string;
	loading?: boolean;
	error?: string;
}

export class User {
	constructor(public userId: string, public userName: string) {}
}
