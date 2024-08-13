using ReactBackend.DTO;
using ReactBackend.Interfaces;

namespace ReactBackend.Services
{
    public class UserFriendService
    {
        private readonly UserFriendsInterface _userFriendInterface;

        public UserFriendService(UserFriendsInterface userFriendInterface)
        {
            _userFriendInterface = userFriendInterface;
        }

        public List<UserDTO> GetFriends(int userId)
        {
            var user = _userFriendInterface.GetFriendsByUserId(userId);
            if (user == null)
            {
                return null;
            }
            return user.Select(UserDTO.MapToDto).ToList();
        }

        public List<UserDTO> getFriendRequests(int userID)
        {
            var user = _userFriendInterface.GetFriendRQByUserId(userID);
            if(user == null)
            {
                return null;
            }
            return user.Select(UserDTO.MapToDto).ToList();
        }

        public bool removeFriendRequest(int fromID, int toID)
        {
            if(_userFriendInterface.deleteFriendRequest(fromID, toID))
            {
                return true;
            }
            return false;
        }

        public bool insertNewFriend(int fromID, int toID)
        {
            if (_userFriendInterface.insertFriend(fromID, toID))
            {
                return true;
            }
            return false;
        }
    }
}