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

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
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

        public List<Messages> getPVTMessages(int userId, int friendId)
        {
            List<Messages> pvtMessages = new List<Messages>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = @"SELECT * FROM tbl_Messages 
                                 WHERE (SenderID = @userId AND ReceiverID = @friendId) 
                                 OR (SenderID = @friendId AND ReceiverID = @userId)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@friendId", friendId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pvtMessages.Add(new Messages
                    {
                        message_ID = reader.GetInt32(0),
                        message_fromID = reader.GetInt32(1),
                        message_toID = reader.GetInt32(2),
                        message = reader.GetString(3),
                        message_date = reader.GetDateTime(4)
                    });
                }
                return pvtMessages;
            }
        }

        public bool checkRecentChat(int userID, int friendID)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = "SELECT * FROM tbl_RecentChats WHERE (FromID = @userID AND ToID = @friendID) OR (FromID = @friendID AND ToID = @userID)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@friendID", friendID);
                cmd.Parameters.AddWithValue("@userID", userID);
                try
                {
                    con.Open();
                    SqlDataReader r = cmd.ExecuteReader();
                    return r.HasRows; // Returns true if any rows exist
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public bool addRecentChat(int userID, int friendID)
        {
            if (!checkRecentChat(userID, friendID))
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
                {
                    string query = "INSERT INTO tbl_RecentChats(FromID, ToID) VALUES (@userID, @friendID)";
                    SqlCommand cmd = new SqlCommand(@query, con);
                    cmd.Parameters.AddWithValue("@friendID", friendID);
                    cmd.Parameters.AddWithValue("@userID", userID);
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
            else
            {
                Console.WriteLine("Recent Chat Exists");
                return false;
            }
        }

        public bool addTextMessage(int userID, int friendID, string text)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = "INSERT INTO tbl_Messages(SenderID, ReceiverID, MessageText) VALUES (@userID, @friendID, @text)";
                SqlCommand cmd = new SqlCommand(@query, con);
                cmd.Parameters.AddWithValue("@friendID", friendID);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@text", text);
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
    }
}
