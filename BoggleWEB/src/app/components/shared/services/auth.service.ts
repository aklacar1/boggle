import { Injectable, Inject } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { BaseService } from "./base.service";

import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs';

import { map, catchError } from 'rxjs/operators';
import { LoginData } from '../types/login-data.type';
import { Authentication } from '../types/authentication.type';
import { RegisterData } from '../types/register-data.type';

@Injectable()
export class AuthService extends BaseService {
    private _authNavStatusSource = new BehaviorSubject<boolean>(false);
    authNavStatus$ = this._authNavStatusSource.asObservable();
    Auth: Authentication = new Authentication();

    private loggedIn = false;
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        super();
    }

    register(data:RegisterData){
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers,withCredentials:true });
        return this.http.post('http://localhost:50374/' + "api/Users/Register", data, options).pipe(
            map((res) => true));
    }

    login(data:LoginData) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });

        return this.http
            .post('http://localhost:50374/' + 'api/Users/Login',
                data, options)
            .pipe(
                map(res => {
                    this.fillAuthData();
                    return res;
                })
            )
    }

    logout() {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });

        return this.http
            .post(
                'http://localhost:50374/' + 'api/Users/Logout',
                null, options)
            .subscribe(
                res => this.Auth = new Authentication(),
                err => console.log(err)
            )
            //.catch(this.handleError);
    }

    fillAuthData() {
        this.Auth = new Authentication();
        if (this.Auth.isAuth == true) return this.Auth;
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.get('http://localhost:50374/' + 'api/Users/Auth', options)
            .pipe(
                map(res => res.json())
            ).subscribe(
                res => {
                    this.Auth.isAuth = true;
                    this.Auth.userId = res.userId;
                    this.Auth.userName = res.userName;
                    this.Auth.roles = res.roles;
                    console.log(this.Auth);
                    return res;
                },
            error => console.log(error)
            );





    //.map(res => {
            //    this.Auth.isAuth = true;
            //    this.Auth.userId = res.userId;
            //    this.Auth.userName = res.userName;
            //    this.Auth.roles = res.roles;
            //    console.log(this.Auth);
            //    return res;
            //}).toPromise().Ca
            //.catch(this.handleError);
    }
}