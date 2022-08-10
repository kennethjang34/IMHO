import {Component, OnInit} from '@angular/core';
import {Post} from 'src/app/post';
import {PostService} from 'src/app/services/post.service';

@Component({
	selector: 'app-feed-home',
	templateUrl: './feed-home.component.html',
	styleUrls: ['./feed-home.component.css']
})
export class FeedHomeComponent implements OnInit {
	posts: Post[] = [];
	constructor(private postService: PostService) {}
	ngOnInit(): void {
		this.getPosts();
	}
	getPosts() {
		this.postService.getPosts().subscribe((posts) => {
			this.posts = posts;


		})



	}
	deletePost(post: Post) {
		this.postService.deletePost(post).subscribe(() => {this.posts = this.posts.filter(p => p.id !== post.id)});
	}
	makePost(post: Post) {
		this.postService.makePost(post).subscribe((post) => {
			this.posts.push(post);
			console.log(post);
		});
	}
	updatePost(post: Post, key: string, newValue: string) {
		post[key] = newValue;
		this.postService.updatePost(post).subscribe();
	}

}
