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
            return _userFriendRepository.GetFriendsByUserId(userId);
        }
    }
}