using ReactBackend.DTO;

namespace ReactBackend.Interfaces
{
    public interface UserFriendsInterface
    {
        List<UserDTO> GetFriendsByUserId(int userId);
    }
}
