import { Component } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';
import { Authentication } from '../shared/types/authentication.type';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent {
    constructor(private auth: AuthService, private router: Router) {
        auth.fillAuthData();
        if (auth.Auth.isAuth==true) {
            this.router.navigate(['movies']);
        }
    }
}
