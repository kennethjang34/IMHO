

import {Action} from '@ngrx/store';
import {Channel} from './channel.model';

export const MAKE_CHANNEL = '[Channel] MAKE_POST';
export const CHANNEL_MADE = '[Channel] POST_MADE';
export const GET_CHANNELS = '[Channel] GET_CHANNELS';
export const CHANNEL_LOADED = '[Channel] CHANNEL_LOADED';
export const CHANNEL_LIST_LOADED = '[Channel] CHANNEL_LIST_LOADED';
export const EDIT_CHANNEL = '[Channel] EDIT_POST';
export const CHANNEL_EDITED = '[Channel] POST_EDITED'
export const DELETE_CHANNEL = '[Channel] DELETE_POST';
export const CHANNEL_DELETED = '[Channel] POST_DELETED '
export const CHANNEL_ERROR = '[Channel] Error';
export const RESET_CHANNELS = '[Channel] RESET_POSTS';
export const GET_POSTS = '[Channel] GET_POSTS';
export const JOIN_CHANNEL = '[Channel] JOIN_CHANNEL';
export const LEAVE_CHANNEL = '[Chanenl] LEAVE_CHANNEL';
export const GET_JOINED_CHANNELS = '[Channel] GET_JOINED_CHANNELS';
export class ResetPosts implements Action {
	readonly type = RESET_CHANNELS;
	constructor(public payload?: any) {}
}
export class GetChannels implements Action {
	readonly type = GET_CHANNELS;
	constructor(public payload?: any) {}
}
export class GetJoinedChannels implements Action {
	readonly type = GET_JOINED_CHANNELS;
	constructor(public payload?: any) {}
}
export class ChannelListLoaded implements Action {
	readonly type = CHANNEL_LIST_LOADED;
	constructor(public payload?: any) {}
}
export class ChannelLoaded implements Action {
	readonly type = CHANNEL_LOADED;
	constructor(public payload?: any) {}
}
export class JoinChannel implements Action {
	readonly type = JOIN_CHANNEL;
	constructor(public payload?: any) {}
}
export class LeaveChannel implements Action {
	readonly type = LEAVE_CHANNEL;
	constructor(public payload?: any) {}
}
export class MakeChannel implements Action {
	readonly type = MAKE_CHANNEL;
	constructor(public payload?: any) {}
}
export class EditChannel implements Action {
	readonly type = MAKE_CHANNEL;
	constructor(public payload?: any) {}
}
export class DeleteChannel implements Action {
	readonly type = DELETE_CHANNEL;
	constructor(public payload?: any) {}
}
export class ChannelError implements Action {
	readonly type = CHANNEL_ERROR;
	constructor(public payload?: any) {}
}
export class GetPosts implements Action {
	readonly type = GET_POSTS;
	constructor(public payload: string) {}
}
export type All
	= ResetPosts | GetChannels | ChannelLoaded | JoinChannel | LeaveChannel
	| ChannelError | GetPosts | ChannelListLoaded | MakeChannel | EditChannel | DeleteChannel | GetJoinedChannels;
