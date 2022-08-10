import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Subscription} from 'rxjs';
import {Post} from 'src/app/post';
@Component({
	selector: 'app-feed-input',
	templateUrl: './feed-input.component.html',
	styleUrls: ['./feed-input.component.css']
})
export class FeedInputComponent implements OnInit {
	@Output() onMakePost: EventEmitter<Post> = new EventEmitter();
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
			Title: this.title,
			Body: this.body,
			ChannelId: this.channelId
			, TagId: this.tagId
		};
		this.onMakePost.emit(newPost);
		this.title = '';
		this.body = '';
		this.tagId = '';
		this.channelId = '';
	}
	constructor() {}
	ngOnInit(): void {

	}
}
