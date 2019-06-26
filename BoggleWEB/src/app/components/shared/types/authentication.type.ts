export class Authentication {
    isAuth: boolean;
    userName: string;
    userId: string;
    roles: string[];
    constructor() {
        this.isAuth = false;
        this.userId = '';
        this.userName = '';
        this.roles = new Array<string>();
    }
}