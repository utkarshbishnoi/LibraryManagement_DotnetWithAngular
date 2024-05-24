import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BooksListComponent } from './books-list/books-list.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AddBookComponent } from './add-book/add-book.component';
import { FormsModule } from '@angular/forms';
import { BookDetailComponent } from './book-details/book-details.component';
import { UserLoginComponent } from './user-login/user-login.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { ToastrModule } from 'ngx-toastr';
import { BorrowedBooksComponent } from './borrowed-books/borrowed-books.component';
import { MyAddedBooksComponent } from './my-added-books/my-added-books.component';
import { StarRatingComponent } from './star-rating/star-rating.component';
@NgModule({
  declarations: [
    AppComponent,
    BooksListComponent,
    AddBookComponent,
    BookDetailComponent,
    UserLoginComponent,
    HeaderComponent,
    HomeComponent,
    BorrowedBooksComponent,
    MyAddedBooksComponent,
    StarRatingComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
