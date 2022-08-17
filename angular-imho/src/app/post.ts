export class Image {
	//caption?: string;
	//source?: string;
	//file?: File;
	imageId?: string;
	postId?: number | string;
	imageName?: string;
	constructor(public source?: string | number, public file?: Blob, public caption?: string) {

	}
}
export interface Post {
	[key: string]: string | number | boolean | File | Array<Image> | undefined;
	id?: number,
	title?: string;
	body?: string;
	channelId?: string;
	tagId?: string;
	//image?: any;
	images?: Array<Image>

	//images?: Array<File>;
	//imageCaptions?: Array<string>;
}

