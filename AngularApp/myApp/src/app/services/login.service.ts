import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable, throwError } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';



@Injectable()
export class LoginService {

    private WEB_API_URL: string = 'http://localhost:4162/api/Login';

    constructor(private _httpService: Http) { }

    login(myUser: User): Observable<any> {
        const myHeaders = new Headers();
        myHeaders.append('Content-Type', 'application/json');
        myHeaders.append('Token', '3B427A22-F268-431E-804F-D30D2B53A6C6');
        myHeaders.append('Username', 'admin');

        const requestOptions = new RequestOptions({ headers: myHeaders });

        return this._httpService.put(this.WEB_API_URL, JSON.stringify(myUser), requestOptions).pipe(
            map((response: Response) =><any>JSON.stringify(response)),
            tap(data => console.log('Los datos que obtuvimos fueron: ' + JSON.stringify(data))),
            catchError(this.handleError));
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }

}