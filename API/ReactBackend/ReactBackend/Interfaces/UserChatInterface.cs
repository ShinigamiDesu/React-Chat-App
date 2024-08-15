using ReactBackend.Entities;

namespace ReactBackend.Interfaces
{
    public interface UserChatInterface
    {
        List<User> getRecentChats(int userId);
    }
}
