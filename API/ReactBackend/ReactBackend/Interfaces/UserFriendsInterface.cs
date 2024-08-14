using ReactBackend.DTO;
using ReactBackend.Entities;

namespace ReactBackend.Interfaces
{
    public interface UserFriendsInterface
    {
        List<User> GetFriendsByUserId(int userId);
        List<User> GetFriendRQByUserId(int userId);
        bool deleteFriendRequest(int fromID, int toID);
        bool insertFriend(int fromID, int toID);
        bool removeFriend(int userID, int friendID);
    }
}
