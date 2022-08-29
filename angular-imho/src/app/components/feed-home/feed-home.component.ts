import {Component, OnInit} from '@angular/core';
import {Post} from 'src/app/post';
import {ImageService} from 'src/app/services/image.service';
import {PostService} from 'src/app/services/post.service';
import {Store} from '@ngrx/store'
import {Image} from 'src/app/post';
import {PostActions, PostQuery, PostState} from 'src/app/state/posts';
import {UsersQuery, UserState} from 'src/app/state/users';
import {Observable} from 'rxjs';
import {User} from 'src/app/state/users';
import {AppState} from 'src/app/state/state';
@Component({
	selector: 'app-feed-home',
	templateUrl: './feed-home.component.html',
	styleUrls: ['./feed-home.component.css']
})
export class FeedHomeComponent implements OnInit {
	postState$: Observable<PostState>;
	posts: Post[];
	userState$: Observable<UserState>;
	constructor(private postService: PostService, private imageService: ImageService, private store: Store<AppState>) {
		this.postState$ = this.store.select(PostQuery.getPostState);
		this.userState$ = this.store.select(UsersQuery.getUser);
	}
	ngOnInit(): void {
		this.postState$.subscribe((state: PostState) => {
			this.posts = state.posts;
		});
		this.getPosts();
	}
	reloadClicked(): void {
		this.getPosts();
	}
	getPosts() {
		this.store.dispatch(new PostActions.GetPosts());
	}
	deletePost(post: Post) {
		this.postService.deletePost(post).subscribe();
	}
	makePost(post: Post) {
		this.store.dispatch(new PostActions.MakePost(post));
	}
	updatePost(post: Post, key: string, newValue: string) {
		post[key] = newValue;
		this.postService.updatePost(post).subscribe();
	}


}
