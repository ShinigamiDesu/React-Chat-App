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

        [HttpGet]
        [Route("FriendRequests/{userId}")]
        public IActionResult getFriendRequests(int userId)
        {
            var friendRQ = _userFriendService.getFriendRequests(userId);
            if (friendRQ != null && friendRQ.Count > 0)
            {
                return Ok(friendRQ);
            }
            return NotFound(new { message = "No friends found" });
        }

        [HttpDelete]
        [Route("DeleteRQ/{fromId}/{toId}")]
        public async Task<IActionResult> deleteRQ(int fromId, int toId)
        {
            if(_userFriendService.removeFriendRequest(fromId, toId))
            {
                return Ok("User Deleted");
            }
            return BadRequest("Could not delete the friend request.");
        }

        [HttpPost]
        [Route("AcceptRQ/{fromId}/{toId}")]
        public async Task<IActionResult> acceptRQ(int fromId, int toId)
        {
            if (_userFriendService.insertNewFriend(fromId, toId))
            {
                return Ok("User Added To Friends");
            }
            return BadRequest("Could not add friend.");
        }

        [HttpDelete]
        [Route("DeleteFriend/{userId}/{friendId}")]
        public async Task<IActionResult> deleteFriend(int userId, int friendId)
        {
            if (_userFriendService.deleteFriend(userId, friendId))
            {
                return Ok(new { message = "User deleted successfully" });
            }
            return BadRequest(new { message = "User deleted failure" });
        }

        [HttpPost]
        [Route("AddFriend/{fromId}/{toId}")]
        public async Task<IActionResult> addFriend(int fromId, int toId)
        {
            if (_userFriendService.addFriend(fromId, toId))
            {
                return Ok("User Added To Friends");
            }
            return BadRequest("Could not add friend.");
        }
    }
}
