export interface Post {
	[key: string]: string | number | boolean | File | undefined;
	id?: number,
	title?: string;
	body?: string;
	channelId?: string;
	tagId?: string;
	image?: any;
}

