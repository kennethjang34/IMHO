import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {Post} from '../post';
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
	constructor(private http: HttpClient) {
		this.headers = new HttpHeaders({'Content-Type': 'application/json'});
		this.httpOptions = {headers: this.headers};
	}
	getPosts(): Observable<Post[]> {
		return this.http.get<Post[]>(this.apiUrl);
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
		//return this.http.post<Post>(this.apiUrl, post, httpOptions);
		return this.http.post<Post>(this.testApiUrl, JSON.stringify(post), this.httpOptions);
	}
}
