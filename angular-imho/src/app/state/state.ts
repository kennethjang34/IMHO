
import {Post} from './posts';
import {User} from './users';

export interface AppState {
	posts: Post[];
	user: User;
}
