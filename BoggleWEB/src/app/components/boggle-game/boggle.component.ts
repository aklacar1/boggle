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
    participants: string[] = new Array<string>();
    startTime: string = null;
    endTime: string = null;
    newWord: string = '';
    constructor(private boggleService: BoggleService, private auth: AuthService,  private route: ActivatedRoute) {
        auth.fillAuthData();
    }

    ngOnInit() {
        this.roomId = this.route.snapshot.paramMap.get("id");

        this.boggleService.getRoomStatusByID(this.roomId).toPromise().then(res => {
            this.startTime = res.startTime;
            this.endTime = res.endTime;
            console.log("STATUS: ", res);
        })
        this.boggleService.getGameRoomParticipantsByRoomId(this.roomId).toPromise().then(res => {
            this.participants = res;
            if(!res.includes(this.auth.Auth.userName)){
                this.boggleService.joinGameRoom(this.roomId).toPromise().then(() => this.participants.push(this.auth.Auth.userName));
            }
        });
        this.boggleService.getGameRoomLettersByRoomId(this.roomId).toPromise().then(res => 
        {
            this.letters = res.map(element => element.letter);
        }
        );
    }

    startGame() {
        this.boggleService.startGame(this.roomId).toPromise().then(() => {
            this.boggleService.getRoomStatusByID(this.roomId).toPromise().then(res => {
                this.startTime = res.startTime;
                this.endTime = res.endTime;
            })
        })
    }

    addWord(){
        this.boggleService.addWord(this.roomId, this.newWord).toPromise().then(res=>{

            this.newWord = "";
        })

    }
}
