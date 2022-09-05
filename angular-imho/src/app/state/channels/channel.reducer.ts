

import {AppState} from '../state';
import {createReducer, on, State, Action} from '@ngrx/store';
import * as channelActions from './channel.actions';
import {Channel, ChannelState} from './channel.model';
import * as postActions from '../posts/post.actions';
export type ChannelAction = channelActions.All | postActions.All;
import {Post, Image, postReducer} from '../posts';
import {PostState} from '../posts/post.model';
import {PostActions} from '../posts';
export type PostAction = postActions.All;

export namespace ChannelQuery {
	export const getChannelState = (state: AppState) => state.channelState;
};
//function sortByDate(channels: Channel[]) {
//}

export function channelReducer(state: ChannelState = {channels: [], loading: true}, action: Action): ChannelState {
	const channelAction = action as ChannelAction | PostAction;
	const channelSelected = {...state.channels[channelAction.payload.channelId]};
	const channelListFiltered = [...state.channels.filter((ch) => ch.channelId !== channelSelected.channelId)];
	switch (channelAction.type) {
		case channelActions.GET_CHANNELS:
			return {...state, loading: true};
		case channelActions.JOIN_CHANNEL:
			return {...state, loading: true};
		case postActions.GET_POSTS:
			return {...state, loading: true};
		case postActions.GET_POSTS:
		//channelSelected.postState = postReducer(channelSelected.postState, action)
		//return {...state, loading: true, channels: [...channelListFiltered, channelSelected]};
		case postActions.MAKE_POST:
		//return {...state, loading: true};
		case postActions.EDIT_POST:
		//return {...state, ...postAction.payload, loading: false};
		case postActions.DELETE_POST:
			//return {...state, loading: true};
			channelSelected.postState = postReducer(channelSelected.postState, action)
			return {...state, loading: true, channels: [...channelListFiltered, channelSelected]};
		case postActions.POST_ERROR:
			return {...state, ...channelAction.payload, loading: false};
		case postActions.POST_MADE:
		case postActions.POST_EDITED:
		case postActions.POST_DELETED:
		case postActions.POSTS_LOADED:
		case postActions.RESET_POSTS:
			channelSelected.postState = postReducer(channelSelected.postState, action)
			return {...state, loading: false, channels: [...channelListFiltered, channelSelected]};
		default:
			return state;
	}
};

