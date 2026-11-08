using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Security;
using DalilAcademy.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalilAcademy.Core.DTOs.Auth;

namespace DalilAcademy.Infra.Service
{
    public  class AuthService(IUserRepository users, IPasswordHasher hasher, ITokenService tokens, IStudentRepository students ) : IAuthService
    {
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto) 
        {
            var user = await users.GetByEmailAsync( dto.Email.Trim().ToLowerInvariant() );
            if (user is null || !hasher.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invaild Email or Password");
            var (token, expiresAt) = tokens.CreateToken(user);
            return new LoginResponseDto(token, expiresAt);

        }
        public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var email = dto.Email.Trim().ToLowerInvariant();

            if (await users.GetByEmailAsync(email) is not null || await students.EmailExistsAsync(email))
                throw new InvalidOperationException("Email already registered");

            var user = new User
            {
                Email = email,
                PasswordHash = hasher.Hash(dto.Password),   // BCrypt, same as login
                Role = Roles.Student                   // never from the client
            };

            await students.CreateAsync(new Student
            {
                FullName = dto.FullName,
                Email = email,
                User = user      // EF saves the user and student together
            });

            var (token, expiresAt) = tokens.CreateToken(user);   // user.Id is filled in after the save
            return new LoginResponseDto(token, expiresAt);
        }

    }
}
