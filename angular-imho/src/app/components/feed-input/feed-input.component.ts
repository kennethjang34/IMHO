import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {Subscription} from 'rxjs';
import {Post, Image} from 'src/app/post';

//class ImageSnippet {
//constructor(public src: string, public file: File, public caption: string) {}
//}
@Component({
	selector: 'app-feed-input',
	templateUrl: './feed-input.component.html',
	styleUrls: ['./feed-input.component.css']
})
export class FeedInputComponent implements OnInit {
	@Output() onMakePost: EventEmitter<Post | FormData> = new EventEmitter();
	title?: string;
	body?: string;
	channelId?: string;
	tagId?: string;
	selectedFile?: Image;
	@ViewChild('imageInput', {static: false})
	imageInput: any
	onSubmit() {
		if (!this.body) {
			alert('body cannot be empty!');
			return;
		}
		const newPost = {
			title: this.title,
			body: this.body,
			channelId: this.channelId, tagId: this.tagId ?? '-1', images: this.selectedFile ? [this.selectedFile] : []
		};
		this.onMakePost.emit(newPost);
		this.title = '';
		this.body = '';
		this.tagId = null;
		this.channelId = null;
		this.selectedFile = null;
		this.imageInput.nativeElement.value = null;
	}
	processImage(event: any, imageInput: any) {
		const file = imageInput.files[0];
		const reader = new FileReader();
		reader.addEventListener('load', (event: any) => {
			this.selectedFile = new Image(event.target.result, file, "NO CAPTION (FOR TEST)");
		});
		reader.readAsDataURL(file);
	}

	constructor() {}
	ngOnInit(): void {

	}
}
