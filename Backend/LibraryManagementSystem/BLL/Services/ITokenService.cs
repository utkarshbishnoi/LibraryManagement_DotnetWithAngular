using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public interface ITokenService
    {
        string GenerateToken(int userId);
        bool VerifyToken(string token);
    }
}
