using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IOperations
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);

        Task<IEnumerable<Book>> GetBooksBorrowedByUserId(int userId);
        Task<IEnumerable<Book>> GetBooksLentByUserId(int userId);
       Task<Book> AddBook(Book entity);

        Book UpdateBook(Book entity);
        void RemoveBook(Book entity);
        User UpdateUser(User entity);
        
        Task<int> SaveChanges();

        Task<User> GetUserById(int userId);

        Task<User> GetUserByUsername(string username);

        Task<IEnumerable<Ratings>> GetRatingsByBookId(int bookId);
        Task<Ratings> AddRating(Ratings rating);
        Task LoadBookRatings(Book book);
    }
}
   