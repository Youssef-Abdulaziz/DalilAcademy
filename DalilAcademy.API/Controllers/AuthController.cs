using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using static DalilAcademy.Core.DTOs.Auth;

namespace DalilAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService auth) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto dto) => Ok(await auth.LoginAsync(dto));


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || dto.Password?.Length < 8)
                return BadRequest("Email is required and password must be at least 8 characters.");

            try
            {
                var result = await auth.RegisterAsync(dto);
                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
