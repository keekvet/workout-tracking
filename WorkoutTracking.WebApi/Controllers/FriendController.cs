using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (await friendService.RemoveFriendAsync(id, userResolverService.GetUserId()))
                return Ok();
            return BadRequest();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetFriendsAsync([FromQuery] FriendPaginationModel model)
        {
            return Ok(await friendService.GetFriendsAsync(model));
        }
    }
}
