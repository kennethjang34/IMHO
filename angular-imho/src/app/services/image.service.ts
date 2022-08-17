import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Injectable, Injector} from '@angular/core';
import {Image} from '../post';
import {map, Observable, of, merge, tap} from 'rxjs';
@Injectable({
	providedIn: 'root'
})
export class ImageService {
	private apiUrl: string;
	private httpHeaders: HttpHeaders;
	private httpOptions: any;
	constructor(private injector: Injector, private http: HttpClient) {
		this.apiUrl = this.injector.get('API_ADDRESS') + "images/";
		this.httpHeaders.append('observe', 'response');
		this.httpHeaders.append('accept', 'image/jpeg');
		//this.httpHeaders.append('Accept', 'image/jpeg');
		this.httpOptions = {headers: this.httpHeaders, responseType: 'blob' as 'json'};
	}
	getImageFiles(images: Array<Image>): Observable<Image> {
		const downloaded: Array<Image> = [];
		const imageDownloaders: Array<Observable<Image>> = [];
		images.forEach(image => {
			imageDownloaders.push(this.getImageFile(image));
		});
		return merge(...imageDownloaders);
	}
	getImageFile(image: Image): Observable<Image> {
		console.log("inside getImageFile: " + image.imageId);
		const imageId = image.imageId;
		//const imageId = 114;
		//let params = new HttpParams().set('post-id', imageId);
		const httpObservable = this.http.get<Blob>(this.apiUrl + `${imageId}`, this.httpOptions);
		//httpObservable.subscribe((v) => {console.log(v)});
		return httpObservable.pipe(map((imageFile: any) => {
			console.log("wowowowowow");
			return new Image(imageId, imageFile, "No caption yet")
		}));
	}
}
