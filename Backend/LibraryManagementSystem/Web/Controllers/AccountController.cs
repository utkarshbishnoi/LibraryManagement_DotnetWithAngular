using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DAL.Entities;
using BLL.Facade;
using BLL.Services;
using DAL.Repository;
using Web.DTO;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IFacadeOperations _userFacade;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;

        public AccountController(IFacadeOperations _userFacade, ITokenService tokenService, IPasswordHashService passwordHashService)
        {
            this._userFacade = _userFacade;
            this._tokenService = tokenService;
            this._passwordHashService = passwordHashService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userFacade.GetUserByUsername(userLogin.Username);
            if (user == null || !_passwordHashService.VerifyPassword(userLogin.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _tokenService.GenerateToken(user.Id);

            var response = new
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Token = token,
                TokensAvailable=user.TokensAvailable,
                BooksBorrowed=user.BorrowedBooks,
                BooksLent=user.LentBooks
            };

            return Ok(response);
        }


    }
}
    