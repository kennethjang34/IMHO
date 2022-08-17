import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Injectable, Injector} from '@angular/core';
import {Image} from '../post';
import {map, Observable, of, merge} from 'rxjs';
@Injectable({
	providedIn: 'root'
})
export class ImageService {
	private apiUrl: string;
	private httpOptions: {headers?: HttpHeaders};
	private httpHeaders: HttpHeaders;
	constructor(private injector: Injector, private http: HttpClient) {
		this.apiUrl = this.injector.get('API_ADDRESS') + "images/";
		console.log(this.apiUrl);
		this.httpHeaders = new HttpHeaders({});
		this.httpOptions = {headers: this.httpHeaders};
	}
	getImageFiles(images: Array<Image>): Observable<Image> {
		const downloaded: Array<Image> = [];
		const imageDownloaders: Array<Observable<Image>> = [];
		images.forEach(image => {
			imageDownloaders.push(this.getImageFile(image));
		});
		return merge(...imageDownloaders)
	}
	getImageFile(image: Image): Observable<Image> {
		//console.log("inside getImageFile: " + image.imageId);
		const imageId = image.imageId;
		let params = new HttpParams().set('post-id', imageId);
		const httpObservable = this.http.get<File>(this.apiUrl + `${imageId}/`);
		return httpObservable.pipe(map((imageFile: File) => {return new Image(imageId, imageFile, "No caption yet")}));
	}
}
