using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactBackend.DTO;
using ReactBackend.Services;

namespace ReactBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChatController : ControllerBase
    {
        private readonly UserChatService _userChatService;

        public UserChatController(UserChatService userChatService)
        {
            _userChatService = userChatService;
        }

        [HttpGet]
        [Route("GetChats/{userId}")]
        public IActionResult getRecentChats(int userId)
        {
            var recentChats = _userChatService.getChats(userId);
            if(recentChats != null && recentChats.Count > 0)
            {
                return Ok(recentChats);
            }
            return NotFound("No Chats Where Found");
        }

        [HttpGet]
        [Route("GetMessages/{userId}/{friendId}")]
        public IActionResult getPVTMessages(int userId, int friendId)
        {
            var messages = _userChatService.getMessages(userId, friendId);
            if (messages != null && messages.Count > 0)
            {
                return Ok(messages);
            }
            return NotFound("No Messages Where Found");
        }

    }
}
