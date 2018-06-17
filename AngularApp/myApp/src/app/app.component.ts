import { Component } from '@angular/core';
import  { UserService } from './services/user.service'; 
import { DocumentService } from './documents/document.service';
import  { LoginService } from './services/login.service'; 
import  { StyleService } from './services/style.service'; 


@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  providers: [UserService, LoginService, StyleService, DocumentService]
})
export class AppComponent {
  title = 'Obligatorio';
}
