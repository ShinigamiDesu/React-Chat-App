using ReactBackend.Entities;

namespace ReactBackend.Interfaces
{
    public interface UserChatInterface
    {
        List<User> getRecentChats(int userId);

        List<Messages> getPVTMessages(int userId, int friendId);

        bool checkRecentChat(int userId, int friendId);

        bool addRecentChat(int userID, int friendID);

        bool addTextMessage(int userID, int friendID, string text);
    }
}
