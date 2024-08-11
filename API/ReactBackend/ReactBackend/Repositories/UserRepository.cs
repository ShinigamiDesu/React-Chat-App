using ReactBackend.Interfaces;
using System.Data.SqlClient;

namespace ReactBackend.Repositories
{
    public class UserRepository : UserInterface
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration){
            _configuration = configuration;
        }

        public bool CreateUser(string username, string password, string profilePicturePath)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string command = "INSERT INTO tbl_User(Username, Password, pfpPath) VALUES (@username, @password, @pfp)";
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@pfp", profilePicturePath);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true; // returns true if the user has been added successfully
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                
            }
        }

        public bool IsUsernameTaken(string username)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = "SELECT COUNT(1) FROM tbl_User WHERE Username = @username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                try
                {
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    con.Close();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
