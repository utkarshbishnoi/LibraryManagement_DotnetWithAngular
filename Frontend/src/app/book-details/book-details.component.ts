import { Component, OnInit } from '@angular/core';
import { Book } from '../models/book.model';
import { BookService } from '../services/Book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Ratings } from '../models/ratings';
import { UserloginService } from '../services/userlogin.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css'],
})
export class BookDetailComponent implements OnInit {
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

  ratings: Ratings[] = [];
  itemid: any;

  rating: Ratings = {
    userId: this.userService.userId,
    bookId: 0,
    rating: 0,
  };

  averageRating: number = 0;
  constructor(
    private bookservice: BookService,
    private userService: UserloginService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id) {
          this.itemid = id;

          this.bookservice.getBook(id).subscribe({
            next: (response) => {
              this.bookDetails = response;
              console.log(this.bookDetails);
              this.rating.bookId = this.bookDetails.id;
            },
          });

          this.bookservice.getRatings(id).subscribe({
            next: (response) => {
              this.ratings = response;
              console.log(this.ratings);

              this.calculateAverageRating();
            },
          });
        }
      },
    });
  }

  calculateAverageRating(): void {
    if (this.ratings.length > 0) {
      const totalRating = this.ratings.reduce(
        (acc, rating) => acc + rating.rating,
        0
      );
      this.averageRating = Math.round(totalRating / this.ratings.length);
    }
  }

  ratebook() {
    const ratingValue = this.rating.rating;

    const rating: Ratings = {
      userId: this.userService.userId,
      bookId: this.bookDetails.id,
      rating: ratingValue,
    };
    console.log(this.rating.rating);
    this.bookservice.rateBook(this.bookDetails.id, rating).subscribe({
      next: (response) => {
        this.toastr.success('Book rated successfully!');
        this.refreshRatings();
      },
      error: (error) => {
        this.toastr.error('Error rating the book. Please try again.');
        console.error('Error rating the book:', error);
      },
    });
  }
  refreshRatings(): void {
    this.bookservice.getRatings(this.itemid).subscribe({
      next: (response) => {
        this.ratings = response;
        this.calculateAverageRating();
      },
    });
  }
}
