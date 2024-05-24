using BLL.Facade;
using BLL.Services;
using DAL.Entities;
using DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.DTO;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IFacadeOperations _facadeOperations;

        public BookController(IFacadeOperations _userFacade)
        {
            this._facadeOperations = _userFacade;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                IEnumerable<Book> books = await _facadeOperations.GetAllBooks();
                return Ok(books);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while fetching the books");
            }
        }
        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookByIdAsync(int bookId)
        {
            try
            {
                Book book = await _facadeOperations.GetBookById(bookId);

                if (book == null)
                {
                    return NotFound("Book not found");
                }

                return Ok(book);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the Book");
            }
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBook([FromBody] BookDTO bookDTO)
        {
            var IdClaim = HttpContext.User.Claims.FirstOrDefault();
            int ID = int.Parse(IdClaim.Value);

            var user = await _facadeOperations.GetUserById(ID);

            if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var book = new Book
                {
                    Name= bookDTO.Name,
                    Author = bookDTO.Author,
                    Genre = bookDTO.Genre,
                    IsBookAvailable = true,
                    Description = bookDTO.Description,
                    LentByUserId=ID

                };
                var newBook = await _facadeOperations.AddBook(book);
            user.TokensAvailable += 1;
            if (newBook == null)
                {
                    return StatusCode(500, "Failed to create book");
                }

            await _facadeOperations.UpdateUser(user);
                return Ok(newBook);
        }
        [Authorize]
        [HttpPut("update/{bookId}")]
        public async Task<IActionResult> UpdateBookAsync(int bookId, [FromBody] UpdateBookDTO updateBookDTO)
        {
            try
            {
                Book book = await _facadeOperations.GetBookById(bookId);

                if (book == null)
                {
                    return NotFound("Book not found");
                }

                if (!string.IsNullOrEmpty(updateBookDTO.Name))
                {
                    book.Name = updateBookDTO.Name;
                }

                if (!string.IsNullOrEmpty(updateBookDTO.Author))
                {
                    book.Author = updateBookDTO.Author;
                }

                if (!string.IsNullOrEmpty(updateBookDTO.Genre))
                {
                    book.Genre = updateBookDTO.Genre;
                }
                if (!string.IsNullOrEmpty(updateBookDTO.Description))
                {
                    book.Description = updateBookDTO.Description;
                }
                var updatedBook = await _facadeOperations.UpdateBook(book);

                return Ok(updatedBook);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the Book");
            }
        }

        [Authorize]
        [HttpDelete("delete/{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            try
            {
                Book book = await _facadeOperations.GetBookById(bookId);

                if (book == null)
                {
                    return NotFound("Book not found");
                }

                await _facadeOperations.RemoveBook(book);

                return Ok(new { message = "Book deleted successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the Book");
            }
        }
        [Authorize]
        [HttpPost("{bookId}/rate")]
        public async Task<IActionResult> RateBook(int bookId, [FromBody] RatingDTO ratingDTO)
        {
            var IdClaim = HttpContext.User.Claims.FirstOrDefault();
            int ID = int.Parse(IdClaim.Value);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _facadeOperations.GetBookById(bookId);
            if (book == null)
            {
                return NotFound($"Book with ID {bookId} not found");
            }

            var user = await _facadeOperations.GetUserById(ID);
            if (user == null)
            {
                return NotFound($"User with ID {ID} not found");
            }

            if (book.Ratings == null)
            {
                book.Ratings = new List<Ratings>();
            }

            if (book.Ratings.Any(r => r.UserId == ID))
            {
                return BadRequest("You have already rated this book");
            }

            var newRating = new Ratings
            {
                UserId = ID,
                BookId = bookId,
                Rating = ratingDTO.Rating
            };

            var success = await _facadeOperations.AddRating(newRating);
            await _facadeOperations.UpdateBook(book);

            if (success)
            {
                return Ok(new { message = "Rating submitted successfully" });
            }
            else
            {
                return BadRequest("Failed to add rating");
            }
        }

        [Authorize]
        [HttpPost("borrow/{bookId}")]
        public async Task<IActionResult> BorrowBook(int bookId)
        {
            var IdClaim = HttpContext.User.Claims.FirstOrDefault();
            int userId = int.Parse(IdClaim.Value);
            try
            {
                if (userId == 0)
                {
                    return Unauthorized("User not logged in");
                }

                var user = await _facadeOperations.GetUserById(userId);

                if (user == null)
                {
                    return Unauthorized("User not found");
                }

                var book = await _facadeOperations.GetBookById(bookId);

                if (book == null)
                {
                    return NotFound("Book not found");
                }

                if (userId == book.LentByUserId)
                {
                    return BadRequest("You are not allowed to borrow a book that you have lent");
                }

                if (!book.IsBookAvailable)
                {
                    return BadRequest("Book is not available for borrowing");
                }

                if (user.TokensAvailable < 1)
                {
                    return BadRequest("User does not have enough tokens to borrow a book");
                }

                book.IsBookAvailable = false;
                book.CurrentlyBorrowedById = user.Id;
                user.TokensAvailable -= 1; 
                await _facadeOperations.UpdateUser(user);

                var lender = await _facadeOperations.GetUserById(book.LentByUserId.Value);

                if (lender != null)
                {
                    lender.TokensAvailable += 1;
                    await _facadeOperations.UpdateUser(lender);
                }

                await _facadeOperations.UpdateBook(book);

                return Ok(new { message="Book borrowed successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpPost("return/{bookId}")]
        public async Task<IActionResult> ReturnBook(int bookId)
        {
            var IdClaim = HttpContext.User.Claims.FirstOrDefault();
            int userId = int.Parse(IdClaim.Value);
            try
            {
                if (userId == 0)
                {
                    return Unauthorized("User not logged in");
                }

                var user = await _facadeOperations.GetUserById(userId);

                if (user == null)
                {
                    return Unauthorized("User not found");
                }

                var book = await _facadeOperations.GetBookById(bookId);

                if (book == null)
                {
                    return NotFound("Book not found");
                }

                if (userId != book.CurrentlyBorrowedById)
                {
                    return BadRequest("You can only return a book that you have borrowed");
                }

                book.IsBookAvailable = true;
                book.CurrentlyBorrowedById = null;
                user.TokensAvailable += 1;
                await _facadeOperations.UpdateUser(user);


                var lender = await _facadeOperations.GetUserById(book.LentByUserId.Value);

                if (lender != null)
                {
                    lender.TokensAvailable -= 1;
                    await _facadeOperations.UpdateUser(lender);
                }

                await _facadeOperations.UpdateBook(book);

                return Ok(new { message = "Book returned successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [Authorize]
        [HttpGet("UserBorrowedBooks")]
        public async Task<IActionResult> GetUserBorrowedBooks()
        {
            var IdClaim = HttpContext.User.Claims.FirstOrDefault();
            int userId = int.Parse(IdClaim.Value);

            try
            {
                var borrowedBooks = await _facadeOperations.GetBooksBorrowedByUserId(userId);

                var userBooks = new { BorrowedBooks = borrowedBooks};

                return Ok(userBooks);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpGet("UserLentBooks")]
        public async Task<IActionResult> GetUserLentBooks()
        {
            var IdClaim = HttpContext.User.Claims.FirstOrDefault();
            int userId = int.Parse(IdClaim.Value);
            try
            {
                var lentBooks = await _facadeOperations.GetBooksLentByUserId(userId);

                var userBooks = new { LentBooks = lentBooks };

                return Ok(userBooks);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("UserDetails/{userId}")]
        public async Task<IActionResult> GetUserDetails(int userId)
        {
            try
            {
                var user = await _facadeOperations.GetUserById(userId);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                var userDetails = new { UserName = user.Name, AvailableTokens = user.TokensAvailable };

                return Ok(userDetails);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpGet("{bookId}/ratings")]
        public async Task<IActionResult> GetRatingsByBookId(int bookId)
        {
            try
            {
                var ratings = await _facadeOperations.GetRatingsByBookId(bookId);
                return Ok(ratings);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
