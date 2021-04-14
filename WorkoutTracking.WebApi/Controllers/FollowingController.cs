using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.User;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/observing")]
    public class FollowingController : ControllerBase
    {
        private readonly IFollowingService followingService;
        private readonly IUserResolverService userResolverService;

        public FollowingController(
            IFollowingService followingService, 
            IUserResolverService userResolverService)
        {
            this.followingService = followingService;
            this.userResolverService = userResolverService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFollowingAsync([FromBody] int userToFollow)
        {
            return this.ConvertResult(await followingService.AddFollowingAsync(userToFollow, userResolverService.GetUserId()));
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFollowingAsync([FromBody] int userToUnfollow)
        {
            return this.ConvertResult(
                await followingService.RemoveFollowingAsync(userToUnfollow, userResolverService.GetUserId()));
        }

        [HttpGet("following")]
        public async Task<IActionResult> GetFollowingAsync([FromQuery] FollowingPaginationModel model)
        {
            return this.ConvertResult(await followingService.GetFollowingAsync(model));
        }

        [HttpGet("followers")]
        public async Task<IActionResult> GetFollowersAsync([FromQuery] FollowingPaginationModel model)
        {
            return this.ConvertResult(await followingService.GetFollowersAsync(model));
        }
    }
}
