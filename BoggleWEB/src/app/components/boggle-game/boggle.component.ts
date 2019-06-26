import { Component, Input, OnInit,Inject } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { BoggleService } from '../shared/services/boggle.service';
import { AuthService } from '../shared/services/auth.service';
import { map, catchError } from 'rxjs/operators';

@Component({
    selector: 'boggle',
    templateUrl: './boggle.component.html'
})
export class BoggleGameComponent implements OnInit {
    //@Input() movies: Movie[] = new Array<Movie>();
    roomId: string = '';
    letters: string[] = new Array<string>();

    constructor(private boggleService: BoggleService, private auth: AuthService,  private route: ActivatedRoute) {
        auth.fillAuthData();
    }

    ngOnInit() {
        this.roomId = this.route.snapshot.paramMap.get("id");
    }
}
