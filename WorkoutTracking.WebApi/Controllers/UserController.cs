using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracking.Data.Context;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Application.Services.Implementations;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Application.Models.Pagination;

namespace Workout_tracking.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserByNameAsync(string name)
        {
            return Ok(await userService.GetUserByNameAsync(name));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] UserSearchModel userSearchModel)
        {
            return Ok(await userService.GetUsersWithNameAsync(userSearchModel));
        }
    }
}
