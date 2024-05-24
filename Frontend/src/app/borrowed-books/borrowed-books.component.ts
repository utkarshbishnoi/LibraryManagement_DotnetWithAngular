import { Component, OnInit } from '@angular/core';
import { BookService } from '../services/Book.service';
import { UserloginService } from '../services/userlogin.service';
import { Book } from '../models/book.model';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-borrowed-books',
  templateUrl: './borrowed-books.component.html',
  styleUrls: ['./borrowed-books.component.css'],
})
export class BorrowedBooksComponent implements OnInit {
  books: Book[] = [];

  constructor(
    private bookService: BookService,
    private loginService: UserloginService,
    private router: Router,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    if (this.loginService.userId) {
      this.bookService
        .getUserBorrowedBooks()
        .subscribe((data) => {
          if (data && data.borrowedBooks && Array.isArray(data.borrowedBooks)) {
            this.books = data.borrowedBooks;
            console.log(this.books);
          } else {
            console.error('BorrowedBooks is not an array:', data);
          }
        });
    } else {
      console.error('User is not logged in.');
    }
  }
  returnBook(bookId: any): void {
    this.bookService.returnBook(bookId).subscribe({
      next: (response) => {
        this.router.navigate(['/books']);
        console.log('Book returned successfully:', response);
        this.toaster.success('Book is returned successfully!');
            
      },
    });
  }
}
