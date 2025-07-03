using Bankamatik.DataAccess.Repositories;
using Bankamatik.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;

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
            var users = _userRepository.GetUsers();
            return Ok(users);
        }

        // GET: api/user/{username}
        [HttpGet("{username}")]
        public IActionResult GetUser(string username)
        {
            var user = _userRepository.GetUser(username);
            if (user == null)
                return NotFound($"User with username '{username}' not found.");

            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public IActionResult InsertUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
                return BadRequest("Username and PasswordHash are required.");

            _userRepository.InsertUser(user.Username, user.PasswordHash);
            return Ok("User inserted successfully.");
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
                return BadRequest("Username and PasswordHash are required.");

            _userRepository.UpdateUser(id, user.Username, user.PasswordHash);
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
