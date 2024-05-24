import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Book } from '../models/book.model';
import { Observable } from 'rxjs/internal/Observable';
import { Router } from '@angular/router';
import { Ratings } from '../models/ratings';
import { UserloginService } from './userlogin.service';
import { catchError, switchMap, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  apiurl = 'http://localhost:5000/api/v1/book';
  user: any;

  constructor(
    private http: HttpClient,
    private router: Router,
    private userLogin: UserloginService
  ) {
    this.user = localStorage.getItem('userInfo');
    if (this.user) {
      this.user = JSON.parse(this.user);
    } else {
      this.router.navigate(['/user-login']);
    }
  }

  getBooks(): Observable<any> {
    return this.http.get<any>(`${this.apiurl}/books`);
  }

  addBook(book: any): Observable<any> {
    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.user.token}`
    );
  
    return this.http.post<Book>(`${this.apiurl}/create`, book, { headers })
      .pipe(
        switchMap(() => this.userLogin.getUserToken()), 
        tap((token) => {
          localStorage.setItem('booktoken', token.availableTokens);
        }),
        catchError((error) => {
          console.error('Error adding book:', error);
          throw error; 
        })
      );
  }

  getBook(id: any): Observable<Book> {
    return this.http.get<Book>(`${this.apiurl}/${id}`);
  }

  updateBook(id: any, book: Book): Observable<Book> {
    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.user.token}`
    );
    return this.http.put<Book>(`${this.apiurl}/update/${id}`, book, {
      headers,
    });
  }

  deleteBook(id: any): Observable<Book> {
    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.user.token}`
    );
    return this.http.delete<Book>(`${this.apiurl}/delete/${id}`, {
      headers,
    });
  }
  rateBook(bookId: any, rating: Ratings): Observable<Ratings> {
    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.user.token}`
    );
    
    return this.http.post<Ratings>(
      `${this.apiurl}/${bookId}/rate`,
      rating,
      {
        headers,
      }
    );
  }
  getUserBorrowedBooks(): Observable<any> {
    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.user.token}`
    );

    return this.http.get<any>(`${this.apiurl}/UserBorrowedBooks`, {
      headers,
    });
  }
  getUserLentBooks(): Observable<any> {
    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.user.token}`
    );

    return this.http.get<any>(`${this.apiurl}/UserLentBooks`, {
      headers,
    });
  }
  getRatings(id: any): Observable<any> {
    return this.http.get<any>(`${this.apiurl}/${id}/ratings`);
  }
  borrowBook(bookId: number): Observable<any> {
    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.user.token}`
    );
    
    return this.http.post<any>(
      `${this.apiurl}/borrow/${bookId}`,
      null,
      {
        headers,
      }
    ).pipe(
      switchMap(() => this.userLogin.getUserToken()), 
      tap((token) => {
        localStorage.setItem('booktoken', token.availableTokens);
      }),
      catchError((error) => {
        console.error('Error borrowing book:', error);
        throw error; 
      })
    );
  }

  returnBook(bookId: number): Observable<any> {
    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.user.token}`
    );

    return this.http.post<any>(
      `${this.apiurl}/return/${bookId}`,
      null,
      {
        headers,
      }
    ).pipe(
      switchMap(() => this.userLogin.getUserToken()), 
      tap((token) => {
        localStorage.setItem('booktoken', token.availableTokens);
      }),
      catchError((error) => {
        console.error('Error returning book:', error);
        throw error; 
      })
    );
  }
}
