using Microsoft.AspNetCore.Mvc;
using ReactBackend.DTO;
using ReactBackend.Services;

namespace ReactBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFriendsController : ControllerBase
    {
        private readonly UserFriendService _userFriendService;

        public UserFriendsController(UserFriendService userFriendService)
        {
            _userFriendService = userFriendService;
        }

        [HttpGet]
        [Route("GetFriends/{userId}")]
        public IActionResult GetFriends(int userId)
        {

            var friends = _userFriendService.GetFriends(userId);
            if (friends != null && friends.Count > 0)
            {
                return Ok(friends);
            }
            return NotFound(new { message = "No friends found" });
        }
    }
}
