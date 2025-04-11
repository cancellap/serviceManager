using SM.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Interfaces
{
    public interface ITokenService
    {
        string HashPassword(string password);
        Task<string> GenerateToken(LoginDto loginDto);
        int GetIdToken(string token);
    }
}
