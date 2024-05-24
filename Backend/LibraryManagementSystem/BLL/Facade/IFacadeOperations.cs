using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public interface IFacadeOperations
    {
        Task<IEnumerable<Book>> GetAllBooks();

        Task<Book> GetBookById(int id);
        Task<IEnumerable<Book>> GetBooksBorrowedByUserId(int userId);
        Task<IEnumerable<Book>> GetBooksLentByUserId(int userId);

        Task<Book> AddBook(Book book);

        Task<Book> UpdateBook(Book book);
        Task<bool> RemoveBook(Book book);
        Task<User> UpdateUser(User user);
        Task<User> GetUserById(int userId);
        Task<User> GetUserByUsername(string username);
        Task<IEnumerable<Ratings>> GetRatingsByBookId(int bookId);
        Task<bool> AddRating(Ratings rating);
        Task LoadBookRatings(Book book);
    }
}
       