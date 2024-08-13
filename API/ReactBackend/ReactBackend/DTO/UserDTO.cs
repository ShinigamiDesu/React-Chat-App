using ReactBackend.Entities;

namespace ReactBackend.DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string PFP { get; set; }
        public string Bio { get; set; }
        public string Status { get; set; }


        public static UserDTO MapToDto(User user)
        {
            return new UserDTO
            {
                ID = user.ID,
                Username = user.Username,
                Bio = user.Bio,
                PFP = Convert.ToBase64String(user.PFP),
                Status = user.Status == 1 ? "Online" : "Offline"
            };
        }


    }
    
    
}
