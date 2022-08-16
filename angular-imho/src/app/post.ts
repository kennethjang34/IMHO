export interface Post {
	[key: string]: string | number | boolean | File | Array<File> | Array<string> | undefined;
	id?: number,
	title?: string;
	body?: string;
	channelId?: string;
	tagId?: string;
	image?: any;
	images?: Array<File>;
	imageCaptions?: Array<string>;
}

