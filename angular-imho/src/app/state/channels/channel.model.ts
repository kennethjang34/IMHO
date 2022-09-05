

import {SafeUrl} from "@angular/platform-browser";
import {Post, PostState} from "../posts";

export interface ChannelState {
	loading: boolean,
	channels: Channel[],
	//images: Image[],
}
export class Channel {
	[key: string]: string | number | boolean | File | Array<any> | PostState | undefined;
	constructor(public channelId: string, public channelName: string, public channelDescription: string,
		public members: string[], public postState: PostState, public posts?: Post[], public tags?: any[]) {
		this.tags = tags ?? [];
		this.posts = posts ?? [];
	}
}
