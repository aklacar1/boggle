import { Component } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';
import { Authentication } from '../shared/types/authentication.type';

@Component({
    selector: 'faq',
    templateUrl: './faq.component.html',
    styleUrls: ['./faq.component.css']
})
export class FaqComponent {
    constructor(private auth: AuthService, private router: Router) {
        auth.fillAuthData();
    }
}
