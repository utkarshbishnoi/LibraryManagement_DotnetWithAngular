import { Component, OnInit } from '@angular/core';
import { Book } from '../models/book.model';
import { BookService } from '../services/Book.service';
import { Router } from '@angular/router';
import { UserloginService } from '../services/userlogin.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.css'],
})
export class BooksListComponent implements OnInit {
  books: Book[] = [];
  isloggedIn: boolean = false;
  userId: number = 0;

  searchInput: string = '';
  currentPage: number = 1;
  pageSize: number = 3;
  totalPages: number = 0;
  selectedCategory: string = 'All';

  constructor(
    private bookService: BookService,
    private router: Router,
    private toastr: ToastrService,
    private userLoginService: UserloginService
  ) {
    this.isloggedIn = this.userLoginService.isloggedIn;
    this.userId = this.userLoginService.userId;
    
  }

  ngOnInit(): void {
    this.loadBooks();
    
  }

  loadBooks(): void {
    this.bookService.getBooks().subscribe({
      next: (books) => {
        this.books = books;
        this.totalPages = Math.ceil(books.length / this.pageSize);
        console.log(books);
        console.log(this.userId);

      },
      error: (response) => {
        console.log(response);
      },
    });
  }

  borrowBook(bookId: any): void {
    this.bookService.borrowBook(bookId).subscribe({
      next: () => {
        this.router.navigate(['borrowed-books']);
        console.log('Book borrowed successfully');
        this.toastr.success('Book borrowed successfully!');
      },
    });
  }

  filterBooks(): void {
    if (this.selectedCategory === 'All') {
      this.totalPages = Math.ceil(this.books.length / this.pageSize);
    } else {
      const filteredBooks = this.books.filter(
        (book) => book.name === this.selectedCategory
      );
      this.totalPages = Math.ceil(filteredBooks.length / this.pageSize);
    }
  }

  searchBooks(): void {
    if (!this.searchInput) {
      this.loadBooks();
    } else {
      this.books = this.books.filter(
        (book) =>
          book.name.toLowerCase().includes(this.searchInput.toLowerCase()) ||
          book.author.toLowerCase().includes(this.searchInput.toLowerCase()) ||
          book.genre.toLowerCase().includes(this.searchInput.toLowerCase())
      );
      this.totalPages = Math.ceil(this.books.length / this.pageSize);
    }
  }

  getCurrentPageBooks(): Book[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    if (this.selectedCategory === 'All') {
      return this.books.slice(startIndex, endIndex);
    } else {
      return this.books
        .filter((book) => book.name === this.selectedCategory)
        .slice(startIndex, endIndex);
    }
  }

  goToPreviousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  goToNextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }
}

