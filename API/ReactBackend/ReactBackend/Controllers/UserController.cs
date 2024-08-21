using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReactBackend.DTO;
using ReactBackend.Entities;
using ReactBackend.Services;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
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
                // Generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); // Use the same key as in Startup
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, userDTO.Username),
                new Claim(ClaimTypes.NameIdentifier, userDTO.ID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1), // Set the token expiration time
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Return the token along with the user information (ID, Username, PFP)
                return Ok(new
                {
                    Token = tokenString,
                    User = new
                    {
                        userDTO.ID,
                        userDTO.Username,
                        userDTO.PFP
                    }
                });
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