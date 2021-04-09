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
using WorkoutTracking.Application.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WorkoutTracking.Application.Dto;
using Workout_tracking.Filters;
using WorkoutTracking.Application.Dto.User;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserResolverService userResolverService;

        public UserController(IUserService userService, IUserResolverService userResolverService)
        {
            this.userService = userService;
            this.userResolverService = userResolverService;
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserByNameAsync(string name)
        {
            return Ok(await userService.GetUserByNameAsync(name));
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUserByNameAsync(int id)
        {
            return Ok(await userService.GetUserByIdAsync(id));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsersAsync([FromQuery] UserSearchPaginationModel userSearchModel)
        {
            return Ok(await userService.GetUsersWithNameAsync(userSearchModel));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateModel userUpdateModel)
        {
            UserDto user = await userService.UpdateUserAsync(userUpdateModel, userResolverService.GetUserId());
            
            if (user is not null)
                return Ok(user);
            
            return BadRequest();
        }

        [CredentialsFilter]
        [HttpPut("update/password")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] PasswordUpdateModel updatePasswordModel)
        {
            bool result = await userService.UpdatePasswordAsync(updatePasswordModel, userResolverService.GetUserId());
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
