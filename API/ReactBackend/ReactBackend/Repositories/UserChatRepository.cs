using ReactBackend.Entities;
using ReactBackend.Interfaces;
using System.Data.SqlClient;

namespace ReactBackend.Repositories
{
    public class UserChatRepository : UserChatInterface
    {
        private readonly IConfiguration _configuration;

        public UserChatRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<User> getRecentChats(int userId)
        {
            List<User> recentChats = new List<User>();

            using(SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = @"SELECT u.UserID, u.Username, u.Bio, u.PFP, u.Status
                                 FROM tbl_User u
                                 JOIN tbl_RecentChats rc ON (u.UserID = rc.FromID AND rc.ToID = @userId)
                                 OR (u.UserID = rc.ToID AND rc.FromID = @userId)
                                 WHERE @userId IN (rc.FromID, rc.ToID)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userId", userId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    recentChats.Add(new User
                    {
                        ID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Bio = reader.GetString(2),
                        PFP = reader["PFP"] as byte[],
                        Status = reader.GetInt32(4)
                    });
                }
                return recentChats;
            }
        }
    }
}
