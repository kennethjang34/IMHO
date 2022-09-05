
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

export interface PostState {
	loading: boolean,
	posts: Post[],
	images: Image[],
	channelId: string
}
export class Post {
	[key: string]: string | number | boolean | File | Array<Image> | undefined;
	//postId?: number,
	//title?: string;
	//body?: string;
	//channelId?: string;
	//tagId?: string;
	////image?: any;
	//images?: Array<Image>
	constructor(public postId?: number, public title?: string, public body?: string, public channelId?: string, public tagId?: string, public images?: Array<Image>, public updatedAt?: string, public createdAt?: string) {}
	//images?: Array<File>;
	//imageCaptions?: Array<string>;
}
