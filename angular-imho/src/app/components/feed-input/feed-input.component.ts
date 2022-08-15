import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Subscription} from 'rxjs';
import {Post} from 'src/app/post';
class ImageSnippet {
	constructor(public src: string, public file: File) {}
}
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
	selectedFile?: ImageSnippet
	onSubmit() {
		if (!this.body) {
			alert('body cannot be empty!');
			return;
		}
		const newPost = {
			title: this.title,
			body: this.body,
			channelId: this.channelId, tagId: this.tagId, image: this.selectedFile?.file
		};
		this.onMakePost.emit(newPost);
		//this.onMakePost.emit(formData);
		this.title = '';
		this.body = '';
		this.tagId = '';
		this.channelId = '';
		this.selectedFile = null;
	}
	processImage(imageInput: any) {
		const file = imageInput.files[0];
		const reader = new FileReader();
		reader.addEventListener('load', (event: any) => {
			this.selectedFile = new ImageSnippet(event.target.result, file);
		});
		reader.readAsDataURL(file);
	}

	constructor() {}
	ngOnInit(): void {

	}
}
