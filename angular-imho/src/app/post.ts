export interface Post {
	[key: string]: string | number | boolean | undefined;
	id?: number,
	title?: string;
	body?: string;
	channelId?: string;
	tagId?: string;
}

