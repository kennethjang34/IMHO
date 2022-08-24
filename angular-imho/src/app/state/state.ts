
import {PostState} from './posts';
import {UserState} from './users';

export interface AppState {
	postState: PostState;
	userState: UserState;
}
