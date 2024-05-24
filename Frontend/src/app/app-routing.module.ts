import { NgModule, importProvidersFrom } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksListComponent } from './books-list/books-list.component';
import { AddBookComponent } from './add-book/add-book.component';
import { BookDetailComponent } from './book-details/book-details.component';
import { UserLoginComponent } from './user-login/user-login.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { BorrowedBooksComponent } from './borrowed-books/borrowed-books.component';
import { MyAddedBooksComponent } from './my-added-books/my-added-books.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'books',
    component: BooksListComponent,
  },
  {
    path: 'add-book',
    component: AddBookComponent,
  },

  {
    path: 'book-details/:id',
    component: BookDetailComponent,
  },
  {
    path: 'user-login',
    component: UserLoginComponent,
  },
  {
    path: 'borrowed-books',
    component: BorrowedBooksComponent,
  },
  {
    path: 'header',
    component: HeaderComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'my-added-books',
    component: MyAddedBooksComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
