namespace ReactBackend.Interfaces
{
    public interface UserInterface
    {
        bool CreateUser(string username, string password, string profilePicturePath);
        bool IsUsernameTaken(string username);
    }
}
