
import {PostState} from './posts';
import {UserState} from './users';
import {ChannelState} from './channels'

export interface AppState {
	postState: PostState;
	userState: UserState;
	channelState: ChannelState;
}
