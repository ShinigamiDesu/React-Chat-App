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
                                WHERE f.UserID = @userId ORDER BY u.Status DESC";

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

        public List<User> GetFriendRQByUserId(int userId)
        {
            List<User> friendRQList = new List<User>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = @"SELECT u.UserID, u.Username, u.Bio, u.PFP
                                 FROM tbl_User u
                                 JOIN tbl_FriendRequests rq ON u.UserID = rq.Request_FromID
                                 WHERE rq.Request_ToID = @userId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userId", userId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    friendRQList.Add(new User
                    {
                        ID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Bio = reader.GetString(2),
                        PFP = reader["PFP"] as byte[],
                    });
                }
                return friendRQList;
            }
        }

        public bool deleteFriendRequest(int fromID, int toID)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = "DELETE FROM tbl_FriendRequests WHERE Request_FromID = @fromID and Request_ToID = @toID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@fromID", fromID);
                cmd.Parameters.AddWithValue("@toID", toID);
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

        public bool insertFriend(int fromID, int toID)
        {
            if (deleteFriendRequest(fromID, toID))
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
                {
                    string query = "INSERT INTO tbl_Friends(UserID, FriendID) VALUES (@userID, @friendID) ; INSERT INTO tbl_Friends(UserID, FriendID) VALUES (@friendID, @userID)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@friendID", toID);
                    cmd.Parameters.AddWithValue("@userID", fromID);
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
                return false;
            }

        }

        public bool removeFriend(int userID, int friendID)
        {

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = "DELETE FROM tbl_Friends WHERE UserID = @userID AND FriendID = @friendID ; DELETE FROM tbl_Friends WHERE UserID = @friendID AND FriendID = @userID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("userID", userID);
                cmd.Parameters.AddWithValue("@friendID", friendID);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close ();
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine (ex.Message);
                    return false;
                }
            }
        }

        public bool checkRequest(int userID, int friendID)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = "SELECT * FROM tbl_FriendRequests WHERE (Request_FromID = @userID AND Request_ToID = @friendID) OR (Request_FromID = @friendID AND Request_ToID = @userID)";
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

        public bool addFriend(int userID, int friendID)
        {
            using(SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ChatApp")))
            {
                string query = "INSERT INTO tbl_FriendRequests(Request_FromID, Request_ToID) VALUES (@userid, @friendid)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userid", userID);
                cmd.Parameters.AddWithValue("@friendid", friendID);
                try
                {
                    if(!checkRequest(userID, friendID))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
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
