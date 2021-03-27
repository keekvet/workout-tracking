using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracking.Data.Context;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Domain.Services.Implementations;
using WorkoutTracking.Domain.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Get User by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserByNameAsync(string name)
        {
            return Ok(await userService.GetUserByNameAsync(name));
        }

        /// <summary>
        /// Get users who's names include "text" parameter from "offset" parameter,
        /// maximum "count" 100 users per request
        /// </summary>
        /// <param name="text"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string text, int offset, int count)
        {
            if (text is null || offset < 0 || count < 1 || count > 100)
                return BadRequest();
            return Ok(await userService.GetUsersRangeWithNameAsync(text, offset, count));
        }
    }
}
