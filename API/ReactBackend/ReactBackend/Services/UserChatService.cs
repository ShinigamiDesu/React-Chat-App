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
    }
}
