using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Operations : IOperations
    {
        private readonly DataContext _context;

        public Operations(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooksBorrowedByUserId(int userId)
        {
            return await _context.Books
                .Where(book => book.CurrentlyBorrowedById == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksLentByUserId(int userId)
        {
            return await _context.Books
                .Where(book => book.LentByUserId == userId)
                .ToListAsync();
        }

        public Task<Book> AddBook(Book entity)
        {
            _context.Books.Add(entity);
            return Task.FromResult(entity);
        }

        public Book UpdateBook(Book entity)
        {
            _context.Books.Update(entity);
            return entity;
        }

        public void RemoveBook(Book entity)
        {
            _context.Books.Remove(entity);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public User UpdateUser(User entity)
        {
            _context.Users.Update(entity);
             return entity;
        }
        
        public async Task<IEnumerable<Ratings>> GetRatingsByBookId(int bookId)
        {
            return await _context.Ratings
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        public Task<Ratings> AddRating(Ratings entity)
        {
            _context.Ratings.Add(entity);
            return Task.FromResult(entity);
        }

        public async Task LoadBookRatings(Book book)
        {
            await _context.Entry(book).Collection(b => b.Ratings).LoadAsync();
        }
    }
}
