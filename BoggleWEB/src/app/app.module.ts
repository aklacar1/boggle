import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { AngularFireMessagingModule } from '@angular/fire/messaging';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AngularFireModule } from '@angular/fire';
import { environment } from '../environments/environment';

import { AuthService } from './components/shared/services/auth.service';
import {BoggleService} from './components/shared/services/boggle.service';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavigationComponent } from './components/shared/navbar/navigation.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FaqComponent } from './components/FAQ/faq.component';
import { BoggleComponent } from './components/boggle/boggle.component';
import { BoggleGameComponent } from './components/boggle-game/boggle.component';
import { MessagingService } from "./components/shared/messaging.service";
import { AsyncPipe } from '../../node_modules/@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavigationComponent,
    LoginComponent,
    RegisterComponent,
    FaqComponent,
    BoggleComponent,
    BoggleGameComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    HttpModule,
    FormsModule,
    TableModule,
    AngularFireDatabaseModule,
    AngularFireAuthModule,
    AngularFireMessagingModule,
    AngularFireModule.initializeApp(environment.firebase),
    RouterModule.forRoot([
        { path: '', redirectTo: 'home', pathMatch: 'full' },
        { path: 'home', component: HomeComponent },
        { path: 'login', component: LoginComponent },
        { path: 'boggle', component: BoggleComponent },
        { path: 'boggle/:id', component: BoggleGameComponent },
        { path: 'signup', component: RegisterComponent },
        { path: 'register', component: RegisterComponent },
        { path: 'faq', component: FaqComponent },
        { path: '**', redirectTo: 'home' }
    ])
  ],
  providers: [
    AuthService,
    BoggleService,
    MessagingService,
    AsyncPipe,
    { provide: 'BASE_URL', useFactory: getBaseUrl }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


export function getBaseUrl():string {
  return 'http://localhost:50374/' as string;
}
