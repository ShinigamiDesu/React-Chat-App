using ReactBackend.DTO;
using ReactBackend.Entities;
using ReactBackend.Interfaces;
using ReactBackend.Repositories;

namespace ReactBackend.Services
{
    public class UserService
    {
        private readonly UserInterface _userInterface;
        private readonly FileInterface _fileInterface;

        public UserService (UserInterface userRepository, FileInterface fileService)
        {
            _userInterface = userRepository;
            _fileInterface = fileService;
        }

        public async Task<bool> RegisterUser(SignUpDTO sign)
        {
            if (_userInterface.IsUsernameTaken(sign.Username))
            {
                return false; // username is taken
            }
            var profilepicture = await _fileInterface.SaveFileAsync(sign.PFP);
            return _userInterface.CreateUser(sign.Username, sign.Password, profilepicture);
        }

        public User LoginUser(string username, string password)
        {
            return _userInterface.GetUserByCredentials(username, password);
        }
    }
}
