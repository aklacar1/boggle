import { Injectable,Inject } from '@angular/core';
import { Http, RequestOptions,Headers } from '@angular/http';
import { map, catchError } from 'rxjs/operators';


@Injectable()
export class BoggleService {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {

    }
    private serviceURL :string= 'http://localhost:50374/';
    createNewGameRoom() {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.post('http://localhost:50374/' +'api/Game', null, options)
            .pipe(map(response => response.json()));

    }
    joinGameRoom(roomId: string) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.patch('http://localhost:50374/' +'api/Game/JoinGameRoom?roomId=' + roomId,null, options)
            .pipe(map(response => response.json()));
    }
    startGame(roomId: string) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.patch('http://localhost:50374/' +'api/Game/StartGame?roomId=' + roomId,null, options)
            .pipe(map(response => response.json()));
    }

    getGameRoomParticipantsByRoomId(roomId: string) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.get('http://localhost:50374/' +'api/Game/GetGameRoomParticipantsByRoomId/' + roomId, options)
            .pipe(map(response => response.json()));
    }

    getRoomStatusByID(roomId: string) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.get('http://localhost:50374/' +'api/Game/GetRoomStatusByID/' + roomId, options)
            .pipe(map(response => response.json()));
    }

    getGameRoomLettersByRoomId(roomId: string) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.get('http://localhost:50374/' +'api/Game/GetGameRoomLettersByRoomId/' + roomId, options)
            .pipe(map(response => response.json()));
    }

    addWord(roomId: string, word: string) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.post('http://localhost:50374/' +'api/Game/AddWord?roomId=' + roomId + '&word='+word, null,options)
            .pipe(map(response => response.json()));
    }

    sendFirebaseToken(token: string) {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
        let options = new RequestOptions({ headers: headers, withCredentials: true });
        this.http.post('http://localhost:50374/' +'api/Firebase?token=' + token, null,options)
            .pipe().toPromise();
    }
}