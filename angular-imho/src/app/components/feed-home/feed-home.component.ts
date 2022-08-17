import {Component, OnInit} from '@angular/core';
import {Post} from 'src/app/post';
import {ImageService} from 'src/app/services/image.service';
import {PostService} from 'src/app/services/post.service';
import {Image} from 'src/app/post';
@Component({
	selector: 'app-feed-home',
	templateUrl: './feed-home.component.html',
	styleUrls: ['./feed-home.component.css']
})
export class FeedHomeComponent implements OnInit {
	posts: Post[] = [];
	constructor(private postService: PostService, private imageService: ImageService) {}
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
		this.postService.makePost(post).subscribe((res) => {
			//change this implementation
			const newPost: Post = {body: res.body, title: res.title, id: res.id, channelId: res.channelId, tagId: res.tagId, images: res.images};
			const downloadedImages: Array<Image> = [];
			this.imageService.getImageFiles([...res.images]).subscribe((img) => {
				newPost.images.push(img);
				downloadedImages.push(img);
			});
			console.log(`downloadedImages: ${downloadedImages}`);
			this.posts.push(newPost);
			this.postService.posts.push(newPost);
			this.posts.forEach(post => {
				console.log(post);
			});
		});
	}
	updatePost(post: Post, key: string, newValue: string) {
		post[key] = newValue;
		this.postService.updatePost(post).subscribe();
	}

}
