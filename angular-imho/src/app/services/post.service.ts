import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {Post} from '../post';
const httpOptions = {
	headers: new HttpHeaders({'Content-Type': 'application/json'})
};
@Injectable({
	providedIn: 'root'
})
export class PostService {
	private apiUrl = 'https://localhost:7089/posts';
	private testApiUrl = 'https://localhost:7089/post/newpost';
	constructor(private http: HttpClient) {}
	getPosts(): Observable<Post[]> {
		return this.http.get<Post[]>(this.apiUrl);
		//const tasks = of(TASKS);
		//return tasks;
	}
	deletePost(post: Post): Observable<Post> {
		const url = `${this.apiUrl}/${post.id}`;
		return this.http.delete<Post>(url);
	}
	updatePost(post: Post): Observable<Post> {
		const url = `${this.apiUrl}/${post.id}`;
		return this.http.put<Post>(url, post, httpOptions);
	}
	makePost(post: Post): Observable<Post> {
		//return this.http.post<Post>(this.apiUrl, post, httpOptions);
		return this.http.post<Post>(this.testApiUrl, JSON.stringify(post), httpOptions);
	}
}
