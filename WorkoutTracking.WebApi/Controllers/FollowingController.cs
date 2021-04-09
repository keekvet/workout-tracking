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
            bool result = await followingService.AddFollowingAsync(userToFollow, userResolverService.GetUserId());

            if(result)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFollowingAsync([FromBody] int userToUnfollow)
        {
            bool result = await followingService.RemoveFollowingAsync(userToUnfollow, userResolverService.GetUserId());
            
            if(result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("following")]
        public async Task<IActionResult> GetFollowingAsync([FromQuery] FollowingPaginationModel model)
        {
            IEnumerable<UserDto> users = await followingService.GetFollowingAsync(model);
            if(users is not null)
                return Ok(users);
            return BadRequest();
        }

        [HttpGet("followers")]
        public async Task<IActionResult> GetFollowersAsync([FromQuery] FollowingPaginationModel model)
        {
            IEnumerable<UserDto> users = await followingService.GetFollowersAsync(model);
            if (users is not null)
                return Ok(users);
            return BadRequest();
        }
    }
}
