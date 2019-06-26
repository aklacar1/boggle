import { Component } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';
import { Authentication } from '../shared/types/authentication.type';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    constructor(private auth: AuthService, private router: Router) {
        auth.fillAuthData();
        if (auth.Auth.isAuth==true) {
            this.router.navigate(['movies']);
        }
    } 
}
