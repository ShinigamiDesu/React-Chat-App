using ReactBackend.Entities;

namespace ReactBackend.Interfaces
{
    public interface UserChatInterface
    {
        List<User> getRecentChats(int userId);

        List<Messages> getPVTMessages(int userId, int friendId);
    }
}
