using ReactBackend.DTO;
using ReactBackend.Entities;

namespace ReactBackend.Interfaces
{
    public interface UserFriendsInterface
    {
        List<User> GetFriendsByUserId(int userId);
    }
}
