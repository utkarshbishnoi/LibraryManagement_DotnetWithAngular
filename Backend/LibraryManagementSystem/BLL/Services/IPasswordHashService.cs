using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }

}
