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

        public List<UserDTO> GetFriendsByUserId(int userId)
        {
            List<UserDTO> friendsList = new List<UserDTO>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = @"SELECT u.UserID, u.Username, u.PFP, u.Status
                                 FROM tbl_User u
                                 JOIN tbl_Friends f ON u.UserID = f.UserID
                                 WHERE f.FriendID = @userId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userId", userId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    friendsList.Add(new UserDTO
                    {
                        ID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Bio = reader.GetString(3),
                        PFP = reader["PFP"] as byte[],
                        Status = reader.GetInt32(3) == 1 ? "Online" : "Offline"
                    });
                }

                return friendsList;
            }
        }

        // Implement other methods like AddFriend, RemoveFriend, etc.
    }
}
