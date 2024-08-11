using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactBackend.Entities;
using ReactBackend.Services;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace ReactBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public RegistrationController(IConfiguration configuration, UserService user)
        {
            _configuration = configuration;
            _userService = user;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromForm] SignUp signup)
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
    }
}