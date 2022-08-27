import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Injectable, Injector} from '@angular/core';
import {Image} from '../post';
import {map, Observable, of, merge, tap} from 'rxjs';
import {DomSanitizer} from '@angular/platform-browser';

@Injectable({
	providedIn: 'root'
})
export class ImageService {
	private apiUrl: string;
	private httpHeaders: HttpHeaders;
	private httpOptions: any;
	constructor(private injector: Injector, private http: HttpClient, private sanitizer: DomSanitizer) {
		this.apiUrl = this.injector.get('API_ADDRESS') + "/images/";
		this.httpHeaders = new HttpHeaders();
		this.httpHeaders.append('observe', 'response');
		this.httpHeaders.append('accept', 'image/jpeg');
		//this.httpHeaders.append('Accept', 'image/jpeg');
		this.httpOptions = {headers: this.httpHeaders, responseType: 'blob' as 'json'};
	}
	getImageFiles(images: Array<Image>): Observable<Image> {
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
		httpObservable.subscribe((v) => {console.log(v)});
		return httpObservable.pipe(map((imageFile: any) => {
			const url = URL.createObjectURL(imageFile);
			const safeUrl = this.sanitizer.bypassSecurityTrustUrl(url);
			return new Image(imageId, imageFile, "No caption yet", safeUrl)
		}));
	}
}
