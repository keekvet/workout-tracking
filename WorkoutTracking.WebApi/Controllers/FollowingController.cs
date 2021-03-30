﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    //[Authorize]
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
        public async Task<IActionResult> AddFollowingAsync([FromBody] FollowingPaginationModel model)
        {
            return Ok(await followingService.AddFollowingAsync(model, userResolverService.GetUserId()));
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFollowingAsync([FromBody] FollowingPaginationModel model)
        {
            return Ok(await followingService.RemoveFollowingAsync(model, userResolverService.GetUserId()));
        }

        [HttpGet("following")]
        public async Task<IActionResult> GetFollowingAsync([FromQuery] FollowingPaginationModel model)
        {
            return Ok(await followingService.GetFollowingAsync(model));
        }

        [HttpGet("follower")]
        public async Task<IActionResult> GetFollowersAsync([FromQuery] FollowingPaginationModel model)
        {
            return Ok(await followingService.GetFollowersAsync(model));
        }
    }
}