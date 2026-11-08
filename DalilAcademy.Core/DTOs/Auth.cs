using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.DTOs
{
    public class Auth
    {
        public record LoginRequestDto(string Email, string Password);
        public record LoginResponseDto(string Token, DateTime ExpiresAt);
        public record RegisterRequestDto(string FullName, string Email, string Password);

    }
}
