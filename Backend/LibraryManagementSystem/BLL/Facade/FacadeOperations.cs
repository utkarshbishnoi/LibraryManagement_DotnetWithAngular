using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;

namespace BLL.Facade    
{
    public class FacadeOperations :  IFacadeOperations
    {
        private readonly DAL.Repository.IOperations _DAL;

        public FacadeOperations(DAL.Repository.IOperations dal)
        {
            _DAL = dal;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            IEnumerable<Book> books = await this._DAL.GetAllBooks();
            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            Book book = await this._DAL.GetBookById(id);
            return book;
        }

        public async Task<IEnumerable<Book>> GetBooksBorrowedByUserId(int userId)
        {
            return await _DAL.GetBooksBorrowedByUserId(userId);
        }

        public async Task<IEnumerable<Book>> GetBooksLentByUserId(int userId)
        {
            return await _DAL.GetBooksLentByUserId(userId);
        }
        public async Task<Book> AddBook(Book book)
        {
            Book newbook = await this._DAL.AddBook(book);
            await this._DAL.SaveChanges();
            return newbook;
        }
        
        public async Task<Book> UpdateBook(Book book)
        {
            Book updatebook = this._DAL.UpdateBook(book);
            await this._DAL.SaveChanges();
            return updatebook;
        }

        public async Task<bool> RemoveBook(Book book)
        {
            this._DAL.RemoveBook(book);
            await this._DAL.SaveChanges();
            return true;
        }

        public async Task<User> GetUserById(int userId)
        {
            User user = await this._DAL.GetUserById(userId);
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            User user = await this._DAL.GetUserByUsername(username);
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            User updatedUser = _DAL.UpdateUser(user);
                await _DAL.SaveChanges();
                return updatedUser;
        }
        
        public async Task<IEnumerable<Ratings>> GetRatingsByBookId(int bookId)
        {
            return await _DAL.GetRatingsByBookId(bookId);
        }

        public async Task<bool> AddRating(Ratings rating)
        {
            var userExists = await _DAL.GetUserById(rating.UserId) != null;
            if (!userExists)
            {
                return false;
            }

            await _DAL.AddRating(rating);
            await _DAL.SaveChanges();
            return true;
            
        }
        
        public async Task LoadBookRatings(Book book)
        {
            await _DAL.LoadBookRatings(book);
        }
    }
}
