import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { Authentication } from '../types/authentication.type';

@Component({
    selector: 'navigation-menu',
    templateUrl: './navigation.component.html',
    styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {

    constructor(private auth: AuthService, private router: Router) {
        auth.fillAuthData();
        this.isAdmin();
    }

    logOut() {
        this.auth.logout();
        this.router.navigate(['home']);
    }

    isActive(viewLocation:string) {
        return viewLocation === this.router.url;
    };

    isAdmin() {

        if (this.auth.Auth.roles != null && this.auth.Auth.roles != undefined && this.auth.Auth.roles.lastIndexOf("SU") != -1)
            return true;
        return false;
    };



}
