using ReactBackend.DTO;
using ReactBackend.Entities;
using ReactBackend.Interfaces;
using System.Data.SqlClient;

namespace ReactBackend.Repositories
{
    public class UserRepository : UserInterface
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
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

        public bool CreateUser(string username, string password, string bio, byte[] profilePicture)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string command = "INSERT INTO tbl_User (Username, Password, Bio, PFP) VALUES (@username, @password, @bio, @pfp)";
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@bio", bio);
                cmd.Parameters.AddWithValue("@pfp", profilePicture);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public User GetUserByCredentials(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = "SELECT * FROM tbl_User WHERE Username = @username AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        ID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Bio = reader.GetString(3),
                        PFP = reader["PFP"] as byte[],  // Retrieve as byte array
                        Status = reader.GetInt32(5)
                    };
                }
                return null; // Return null if no matching user is found
            }
        }

        public List<User> getSearchedUser(string Username)
        {
            List<User> searchedUser = new List<User>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = @"SELECT u.UserID, u.Username, u.Bio, u.PFP
                                FROM tbl_User u
                                WHERE u.Username LIKE @username";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", '%' + Username + "%");

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    searchedUser.Add(new User
                    {
                        ID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Bio = reader.GetString(2),
                        PFP = reader["PFP"] as byte[],
                    });
                }
                return searchedUser;
            }
        }

    }
}
