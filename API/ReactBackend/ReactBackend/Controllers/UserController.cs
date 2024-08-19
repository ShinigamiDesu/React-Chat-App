using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactBackend.DTO;
using ReactBackend.Entities;
using ReactBackend.Services;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace ReactBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public UserController(IConfiguration configuration, UserService user)
        {
            _configuration = configuration;
            _userService = user;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromForm] SignUpDTO signup)
        {
            if (await _userService.RegisterUser(signup))
            {
                return Ok("Registration successful");
            }
            else
            {
                return Conflict("Username is already taken");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var userDTO = _userService.LoginUser(login.Username, login.Password);
            if (userDTO != null)
            {
                return Ok(userDTO);
            }
            return Unauthorized(new { message = "Invalid username or password" });
        }

        [HttpGet]
        [Route("GetUsers/{username}")]
        public IActionResult getUserSearched(string username)
        {
            var user = _userService.getUsersSearched(username);
            if (user != null && user.Count > 0)
            {
                return Ok(user);
            }
            return NotFound("No Users");
        }
    }
}