import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserloginService } from '../services/userlogin.service';
import { BookService } from '../services/Book.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css'],
})
export class UserLoginComponent {
  cachedData: any;
  errorMessage: string = '';

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private authService: UserloginService,
    private bookservice: BookService
  ) {}

  login(data: any): void {
    this.authService.login(data.username, data.password).subscribe(
      (returnData) => {
        this.cachedData = returnData;

        if (this.cachedData.token) {
          this.authService.username = this.cachedData.username;
          this.authService.isloggedIn = true;
          localStorage.setItem('userInfo', JSON.stringify(this.cachedData));
          localStorage.setItem('booktoken', JSON.stringify(this.cachedData.tokensAvailable));
          this.router.navigate(['/books']);
          this.toastr.success('logged In successfully!');
        }
      },
      (error) => {
        this.errorMessage = 'Invalid username or password';
      }
    );
  }
  clearErrorMessage(): void {
    this.errorMessage = '';
  }
  ngOnInit(): void {}
}
