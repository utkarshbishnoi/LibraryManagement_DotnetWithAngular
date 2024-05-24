import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class UserloginService {
  private apiUrl = 'http://localhost:5000/api/v1/account/login';
  user: any;
  tokens: any;
  username: string = '';
  isloggedIn: boolean = false;
  userId: number = 0;

  constructor(private http: HttpClient) {
    this.user = localStorage.getItem('userInfo');
    this.user = JSON.parse(this.user);
    if (this.user) {
      this.username = this.user.username;
      this.isloggedIn = true;
      this.tokens = this.user.tokensAvailable;
      this.userId = this.user.id;
    }
  }

  login(username: string, password: string): Observable<any> {
    const loginData = { username, password };
    return this.http.post<any>(this.apiUrl, loginData).pipe(
      tap((returnData) => {
        if (returnData.token) {
          this.username = returnData.username;
          this.isloggedIn = true;
          this.userId = returnData.id;
          this.tokens = returnData.tokensAvailable;
          localStorage.setItem('userInfo', JSON.stringify(returnData));
          localStorage.setItem(
            'booktoken',
            JSON.stringify(returnData.tokensAvailable)
          );
        }
      })
    );
  }
  logout(): void {
    localStorage.removeItem('userInfo');
    localStorage.removeItem('booktoken');
    this.user = null;
    this.username = '';
    this.isloggedIn = false;
    this.userId = 0;
    
  }

  getUserToken() : Observable<any> {
    return this.http.get<any>(
      `http://localhost:5000/api/v1/book/UserDetails/${this.user.id}`
    );
  }
}
