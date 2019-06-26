import { Component, Input, OnInit,Inject } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { BoggleService } from '../shared/services/boggle.service';
import { AuthService } from '../shared/services/auth.service';
import { map, catchError } from 'rxjs/operators';

@Component({
    selector: 'boggle',
    templateUrl: './boggle.component.html'
})
export class BoggleComponent implements OnInit {
    //@Input() movies: Movie[] = new Array<Movie>();
    roomId: string = '';
    letters: string[] = new Array<string>();

    constructor(private boggleService: BoggleService, private auth: AuthService,  private router: Router) {
        auth.fillAuthData();
    }

    ngOnInit() {
        //this.getTopRatedMovies();
    }

    createNewGameRoom() {
        this.boggleService.createNewGameRoom()
        .pipe(map(res => res))
        .toPromise()
        .then(room => 
            {
                this.roomId = room[0].gameRoomId;
                this.letters = [];
                this.letters = room.map(element => element.letter);
            }
        ).then(res => this.router.navigate(['boggle', this.roomId]));
    }

    // getTopRatedMovies() {
    //     console.log("getTopRated");
    //     this.movieService.getTopRatedMovies().map(res => res as Movie[]).toPromise().then(movies => this.movies = movies);
    // }
}
