using ReactBackend.DTO;
using ReactBackend.Entities;
using ReactBackend.Interfaces;
using System.Data.SqlClient;

namespace ReactBackend.Repositories
{
    public class UserFriendRepository : UserFriendsInterface
    {
        private readonly IConfiguration _configuration;

        public UserFriendRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<User> GetFriendsByUserId(int userId)
        {
            List<User> friendsList = new List<User>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = @"SELECT u.UserID, u.Username, u.Bio, u.PFP, u.Status
                                FROM tbl_User u
                                JOIN tbl_Friends f ON u.UserID = f.FriendID
                                WHERE f.UserID = @userId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userId", userId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    friendsList.Add(new User
                    {
                        ID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Bio = reader.GetString(2),
                        PFP = reader["PFP"] as byte[],
                        Status = reader.GetInt32(4)
                    });
                }

                return friendsList;
            }
        }

        // Implement other methods like AddFriend, RemoveFriend, etc.
    }
}
