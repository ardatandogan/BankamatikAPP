using Microsoft.AspNetCore.Mvc;
using Bankamatik.Core.DTOs;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;

namespace Bankamatik.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public AuthController(IConfiguration configuration)
        {
            _userRepository = new UserRepository(configuration);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.PasswordHash))
                return BadRequest("Username and PasswordHash are required.");

            var user = _userRepository.GetUser(new User { Username = loginDto.Username });


            if (user == null || user.PasswordHash != loginDto.PasswordHash)
                return Unauthorized("Invalid username or password.");

            // Giriş başarılı, şimdilik sade bir dönüş
            return Ok(new
            {
                Message = "Login successful",
                UserID = user.ID,
                Username = user.Username
                // Token vb. eklenecekse buraya eklenir
            });
        }
    }
}
