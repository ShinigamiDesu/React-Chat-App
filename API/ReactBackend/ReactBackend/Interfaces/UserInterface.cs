using ReactBackend.Entities;

namespace ReactBackend.Interfaces
{
    public interface UserInterface
    {
        bool CreateUser(string username, string password, string bio, byte[] profilePicturePath);
        bool IsUsernameTaken(string username);
        User GetUserByCredentials(string username, string password);
        public List<User> getSearchedUser(string Username);
    }
}
