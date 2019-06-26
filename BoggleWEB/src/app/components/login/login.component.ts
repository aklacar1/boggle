import { Component } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';
import { LoginData } from '../shared/types/login-data.type';
import { Authentication } from '../shared/types/authentication.type';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    constructor(private auth: AuthService, private router: Router) {
        auth.fillAuthData();
        if (auth.Auth.isAuth == true) {
            this.router.navigate(['movies']);
        }
    }
    private loginData: LoginData = new LoginData();
    private message: string = '';
    login() {
        this.message = '';
        this.loginData.rememberMe = true;
        console.log(this.loginData);
        if (this.loginData.userName == '' || this.loginData.password == '') {
            this.message = 'Please fill out the login data';
        }
        this.auth.login(this.loginData).toPromise().then(res => this.router.navigate(['movies']));
    }


}
