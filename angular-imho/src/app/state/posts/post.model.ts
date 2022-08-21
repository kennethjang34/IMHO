
import {SafeUrl} from "@angular/platform-browser";

export class Image {
	//caption?: string;
	//source?: string;
	//file?: File;
	imageId?: string;
	postId?: number | string;
	imageName?: string;
	imageUrl?: SafeUrl;
	constructor(public source?: string | number, public file?: Blob, public caption?: string, public url?: SafeUrl) {
		//this.imageUrl = URL.createObjectURL(file);
		this.imageUrl = url;
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
