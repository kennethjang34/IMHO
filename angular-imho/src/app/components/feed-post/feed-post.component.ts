import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import {Post} from '../../post';
@Component({
	selector: 'app-feed-post',
	templateUrl: './feed-post.component.html',
	styleUrls: ['./feed-post.component.css', '../feed-home/feed-home.component.css']
})
export class FeedPostComponent implements OnInit {

	@Input() post?: Post
	@Output() onDeletePost: EventEmitter<Post> = new EventEmitter();
	//@Output() onUpdatePost: EventEmitter<Post> = new EventEmitter();
	constructor() {}
	onDelete(post?: Post) {
		this.onDeletePost.emit(post);
	}
	ngOnInit(): void {
	}

}
