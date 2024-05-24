import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserloginService } from '../services/userlogin.service';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  animations: [
    trigger('fadeInOut', [
      state('in', style({ opacity: 1 })),
      transition(':enter', [style({ opacity: 0 }), animate(300)]),
      transition(':leave', animate(300, style({ opacity: 0 }))),
    ]),
  ],
})
export class HeaderComponent implements OnInit {
  isAdmin: boolean = false;
  userName: string = '';
  isloggedIn: boolean = false;
  tokens: number = 0;

  constructor(
    private authService: UserloginService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.isloggedIn = this.authService.isloggedIn;
    this.userName = this.authService.username;
    this.tokens = this.authService.tokens;
    var tokenAvail = localStorage.getItem('booktoken');
    if (tokenAvail) this.tokens = parseInt(tokenAvail);
  }
  ngOnInit(): void {}
  logout() {
    this.authService.logout();
    this.router.navigate(['']);
    this.toastr.success('Log Out successfully!');
    console.log('logout successfully');
  }
}
