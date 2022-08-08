import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Subscription} from 'rxjs';
import {Post} from 'src/app/post';
@Component({
	selector: 'app-feed-input',
	templateUrl: './feed-input.component.html',
	styleUrls: ['./feed-input.component.css']
})
export class FeedInputComponent implements OnInit {
	@Output() onNewPost: EventEmitter<Post> = new EventEmitter();
	title?: string;
	body?: string;
	channelId?: string;
	tagId?: string;
	onSubmit() {
		if (!this.body) {
			alert('body cannot be empty!');
			return;
		}
		const newPost = {
			title: this.title,
			body: this.body,
			channelId: this.channelId,
			tagId: this.tagId
		};
		this.onNewPost.emit(newPost);
		this.title = '';
		this.body = '';
		this.tagId = '';
		this.channelId = '';
	}
	constructor() {}
	ngOnInit(): void {

	}
}
