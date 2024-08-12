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
        public UserService (UserInterface userRepository, FileInterface fileInterface)
        {
            _userInterface = userRepository;
            _fileInterface = fileInterface;
        }

        public async Task<bool> RegisterUser(SignUpDTO sign)
        {
            if (_userInterface.IsUsernameTaken(sign.Username))
            {
                return false; // username is taken
            }
            byte[] profilePictureBytes = await _fileInterface.ConvertToByteArrayAsync(sign.PFP);
            return _userInterface.CreateUser(sign.Username, sign.Password, sign.Bio, profilePictureBytes);
        }

        public UserDTO LoginUser(string username, string password)
        {
            var user = _userInterface.GetUserByCredentials(username, password);
            if (user == null)
            {
                return null;
            }

            return UserDTO.MapToDto(user);
        }
    }
}
