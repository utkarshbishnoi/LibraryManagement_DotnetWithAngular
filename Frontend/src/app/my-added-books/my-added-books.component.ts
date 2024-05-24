import { Component, OnInit } from '@angular/core';
import { BookService } from '../services/Book.service';
import { UserloginService } from '../services/userlogin.service';
import { Book } from '../models/book.model';

@Component({
  selector: 'app-my-added-books',
  templateUrl: './my-added-books.component.html',
  styleUrls: ['./my-added-books.component.css'],
})
export class MyAddedBooksComponent implements OnInit {
  books: Book[] = [];

  constructor(
    private bookService: BookService,
    private loginService: UserloginService
  ) {}

  ngOnInit(): void {
    this.bookService
      .getUserLentBooks()
      .subscribe((data) => {  
        if (data && data.lentBooks && Array.isArray(data.lentBooks)) {
          this.books = data.lentBooks;
          console.log(this.books);
        } else {
          console.error('lentBooks is not an array:', data);
        }
        console.log(this.books);
      });
  }
}
