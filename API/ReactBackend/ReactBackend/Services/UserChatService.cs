using ReactBackend.DTO;
using ReactBackend.Interfaces;

namespace ReactBackend.Services
{
    public class UserChatService
    {
        private readonly UserChatInterface _userChatInterface;

        public UserChatService(UserChatInterface userChatInterface)
        {
            _userChatInterface = userChatInterface;
        }

        public List<UserDTO> getChats(int userId)
        {
            var chats = _userChatInterface.getRecentChats(userId);
            if(chats == null)
            {
                return null;
            }
            return chats.Select(UserDTO.MapToDto).ToList();
        }

        public List<MessagesDTO> getMessages(int userId, int friendId)
        {
            var messages = _userChatInterface.getPVTMessages(userId, friendId);
            if (messages == null)
            {
                return null;
            }
            return messages.Select(MessagesDTO.MapToDto).ToList();
        }

        public bool newRecentChat(int userID, int friendID)
        {
            if(_userChatInterface.addRecentChat(userID, friendID))
            {
                return true;
            }
            return false;
        }

        public bool newMessage(int userID, int friendID, string text)
        {
            if(_userChatInterface.addTextMessage(userID, friendID, text))
            {
                return true;
            }
            return false;
        }
    }
}
