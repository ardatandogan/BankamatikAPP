using Bankamatik.Core.DTOs;
using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace BankamatikAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _userRepository = new UserRepository(configuration);
        }

        // GET: api/user
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers()
                .Select(u => new UserDTO
                {
                    ID = u.ID,
                    Username = u.Username
                })
                .ToList();

            return Ok(users);
        }

        // GET: api/user/{username}
        [HttpGet("{username}")]
        public IActionResult GetUser(string username)
        {
            var userEntity = new User { Username = username };
            var user = _userRepository.GetUser(userEntity);

            if (user == null)
                return NotFound($"User with username '{username}' not found.");

            var userDto = new UserDTO
            {
                ID = user.ID,
                Username = user.Username
            };

            return Ok(userDto);
        }

        // POST: api/user
        [HttpPost]
        public IActionResult InsertUser([FromBody] UserLoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.PasswordHash))
                return BadRequest("Username and PasswordHash are required.");

            var user = new User
            {
                Username = loginDto.Username,
                PasswordHash = loginDto.PasswordHash
            };

            _userRepository.InsertUser(user);
            return Ok("User inserted successfully.");
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserLoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.PasswordHash))
                return BadRequest("Username and PasswordHash are required.");

            var user = new User
            {
                ID = id,
                Username = loginDto.Username,
                PasswordHash = loginDto.PasswordHash
            };

            _userRepository.UpdateUser(user);
            return Ok("User updated successfully.");
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
            return Ok("User deleted successfully.");
        }
    }
}
