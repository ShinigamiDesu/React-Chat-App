using ReactBackend.Entities;
using ReactBackend.Interfaces;

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

        public async Task<bool> RegisterUser(SignUp sign)
        {
            if (_userInterface.IsUsernameTaken(sign.Username))
            {
                return false; // username is taken
            }
            var profilepicture = await _fileInterface.SaveFileAsync(sign.PFP);
            return _userInterface.CreateUser(sign.Username, sign.Password, profilepicture);
        }
    }
}
