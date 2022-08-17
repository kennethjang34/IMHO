import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {map, Observable, of} from 'rxjs';
import {Post} from '../post';
import {ImageService} from './image.service';
//const httpOptions = {
//headers: new HttpHeaders({'Content-Type': 'application/json'})
//};
@Injectable({
	providedIn: 'root'
})
export class PostService {
	private headers: HttpHeaders;
	private apiUrl = 'https://localhost:7089/posts';
	private testApiUrl = 'https://localhost:7089/post/newpost';
	private httpOptions: {headers?: HttpHeaders};
	posts: Post[] = [];
	constructor(private http: HttpClient, private imageService: ImageService) {
		//this.headers = new HttpHeaders({'Content-Type': 'multipart/form-data', 'Accept': 'multipart/form-data'});
		this.headers = new HttpHeaders({});
		this.httpOptions = {headers: this.headers};
	}
	getPosts(): Observable<Post[]> {
		return this.http.get<Post[]>(this.apiUrl).pipe(map((posts: Array<Post>): Array<Post> => {
			posts.forEach(post => {
				this.imageService.getImageFiles(post.images);
			});
			return posts;
		}));
	}
	deletePost(post: Post): Observable<Post> {
		const url = `${this.apiUrl}/${post.id}`;
		return this.http.delete<Post>(url);
	}
	updatePost(post: Post): Observable<Post> {
		const url = `${this.apiUrl}/${post.id}`;
		return this.http.put<Post>(url, post, this.httpOptions);
	}
	makePost(post: Post): Observable<Post> {
		var formData = new FormData();
		formData.append("Title", post.title);
		formData.append("Body", post.body);
		formData.append("ChannelId", post.channelId);
		formData.append("TagId", post.tagId);
		//formData.append("Images", post.image);
		post.images?.forEach(image => {
			formData.append("Images", image.file);
			formData.append("ImageCaptions", image.caption);
		});
		return this.http.post<Post>(this.testApiUrl, formData, this.httpOptions);
	}
	makePostForm(formData: FormData): Observable<FormData> {
		return this.http.post<FormData>(this.testApiUrl, formData, this.httpOptions);
	}

}
