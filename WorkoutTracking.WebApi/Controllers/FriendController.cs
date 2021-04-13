using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.User;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/friend")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService friendService;
        private readonly IUserResolverService userResolverService;

        public FriendController(IFriendService friendService, IUserResolverService userResolverService)
        {
            this.friendService = friendService;
            this.userResolverService = userResolverService;
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFriendAsync([FromBody] int id)
        {
            bool result = await friendService.RemoveFriendAsync(id, userResolverService.GetUserId());
            if (result)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetFriendsAsync([FromQuery] FriendPaginationModel model)
        {
            IEnumerable<UserDto> result = await friendService.GetFriendsAsync(model);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }
    }
}
