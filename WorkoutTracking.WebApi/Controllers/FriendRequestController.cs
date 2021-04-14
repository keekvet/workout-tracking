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
using WorkoutTracking.Application.Models.FriendRequest;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Enums;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/friend-request")]
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
            return this.ConvertResult(
                await friendRequestService.AddFriendRequestAsync(receiverId, userResolverService.GetUserId()));
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFriendRequestAsync([FromBody] int friendId)
        {
            return this.ConvertResult(
                await friendRequestService.RemoveFriendRequestAsync(friendId, userResolverService.GetUserId()));
        }


        [HttpPut("update-state")]
        public async Task<IActionResult> UpdateFriendRequestStateAsync([FromBody] FriendRequestStateUpdateModel model)
        {
            return this.ConvertResult(
                await friendRequestService.UpdateFriendRequestStateAsync(model, userResolverService.GetUserId()));
        }

        [HttpGet("input")]
        public async Task<IActionResult> GetInputRequestsAsync([FromQuery] SortedPaginationModel model)
        {
            return this.ConvertResult(
                await friendRequestService.GetInputFriendRequestsAsync(model, userResolverService.GetUserId()));
        }

        [HttpGet("output")]
        public async Task<IActionResult> GetOutputRequestsAsync([FromQuery] SortedPaginationModel model)
        {
            return this.ConvertResult(
                await friendRequestService.GetOutputFriendRequestsAsync(model, userResolverService.GetUserId()));
        }
    }
}
