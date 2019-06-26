import { Component } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';
import { Authentication } from '../shared/types/authentication.type';
import { MessagingService } from "../shared/messaging.service";

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    constructor(private auth: AuthService, private router: Router, private messagingService: MessagingService) {
        auth.fillAuthData();
        if (auth.Auth.isAuth==true) {
            this.router.navigate(['boggle']);
        }
    } 

}
