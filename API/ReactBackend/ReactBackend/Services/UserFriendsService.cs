using ReactBackend.DTO;
using ReactBackend.Interfaces;

namespace ReactBackend.Services
{
    public class UserFriendService
    {
        private readonly UserFriendsInterface _userFriendRepository;

        public UserFriendService(UserFriendsInterface userFriendInterface)
        {
            _userFriendRepository = userFriendInterface;
        }

        public List<UserDTO> GetFriends(int userId)
        {
            var user = _userFriendRepository.GetFriendsByUserId(userId);
            if (user == null)
            {
                return null;
            }
            return user.Select(UserDTO.MapToDto).ToList();
        }
    }
}