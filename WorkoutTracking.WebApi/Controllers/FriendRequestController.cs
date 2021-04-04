using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.FriendRequest;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Enums;

namespace Workout_tracking.Controllers
{
    [Route("api/friend-request")]
    [ApiController]
    public class FriendRequestController : ControllerBase
    {
        private readonly IUserResolverService userResolverService;
        private readonly IFriendRequestService friendRequestService;

        public FriendRequestController(
            IUserResolverService userResolverService,
            IFriendRequestService friendRequestSevice)
        {
            this.userResolverService = userResolverService;
            this.friendRequestService = friendRequestSevice;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendFriendRequestAsync([FromBody] int receiverId)
        {
            FriendRequestDto friendRequestDto =
                await friendRequestService.AddFriendRequestAsync(receiverId, userResolverService.GetUserId());

            if (friendRequestDto is null)
                return BadRequest();

            return Ok(friendRequestDto);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFriendRequestAsync([FromBody] int friendId)
        {
            if (await friendRequestService.RemoveFriendRequestAsync(friendId, userResolverService.GetUserId()))
                return Ok();
            return BadRequest();
        }


        [HttpPut("update-state")]
        public async Task<IActionResult> UpdateFriendRequestStateAsync([FromBody] FriendRequestStateUpdateModel model)
        {
            FriendRequestDto friendRequest =
                await friendRequestService.UpdateFriendRequestStateAsync(model, userResolverService.GetUserId());

            if (friendRequest is null)
                return BadRequest();
            return Ok(friendRequest);
        }

        [HttpGet("input")]
        public async Task<IActionResult> GetInputRequestsAsync([FromQuery] SortedPaginationModel model)
        {
            return Ok(await friendRequestService.GetInputFriendRequestsAsync(model, userResolverService.GetUserId()));
        }
    
        [HttpGet("output")]
        public async Task<IActionResult> GetOutputRequestsAsync([FromQuery] SortedPaginationModel model)
        {
            return Ok(await friendRequestService.GetOutputFriendRequestsAsync(model, userResolverService.GetUserId()));
        }
    }
}
