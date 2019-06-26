import { Component } from '@angular/core';
import { MessagingService } from "./components/shared/messaging.service";
import { AuthService } from './components/shared/services/auth.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Boggle Game';
  message;
  constructor(private messagingService: MessagingService, private auth: AuthService) { }

  ngOnInit() {
      this.messagingService.receiveMessage();
      this.message = this.messagingService.currentMessage;
  }
}
