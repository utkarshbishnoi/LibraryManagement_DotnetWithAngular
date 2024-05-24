import { Component, OnInit } from '@angular/core';
import { Book } from '../models/book.model';
import { BookService } from '../services/Book.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css'],
})
export class AddBookComponent implements OnInit {
  constructor(
    private bookService: BookService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  bookDetails: Book = {
    id: 0,
    name: '',
    author: '',
    genre: '',
    description: '',
    isBookAvailable: true,
    lentByUserId: 0,
    borrowUserId: 0,
  };
  ngOnInit(): void {}
  onSubmit() {
    this.bookService.addBook(this.bookDetails).subscribe(
      () => {
        console.log('Book:', this.bookDetails);
        this.toastr.success('Book added successfully!');
        this.router.navigate(['/books']);
      },
      (error) => {
        this.toastr.error('Book is not added successfully!');
        console.log('An error occurred:', error);
      }
    );
  }
}
