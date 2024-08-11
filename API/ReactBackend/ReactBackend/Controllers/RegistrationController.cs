using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactBackend.Entities;
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

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromForm] SignUp signup)
        {
            string profilePicturePath = null;
            if (signup.PFP != null)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                var filePath = Path.Combine(uploads, signup.PFP.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await signup.PFP.CopyToAsync(stream);
                }
                profilePicturePath = filePath;
            }

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string command = "INSERT INTO tbl_User(Username, Password, pfpPath) VALUES (@username, @password, @pfp)";
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.Parameters.AddWithValue("@username", signup.Username);
                cmd.Parameters.AddWithValue("@password", signup.Password);
                cmd.Parameters.AddWithValue("@pfp", profilePicturePath);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return Ok("Registration successful");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "Registration failed", error = ex.Message });
                }
            }
        }
    }
}