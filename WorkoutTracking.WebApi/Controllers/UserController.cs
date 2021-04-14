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
            return this.ConvertResult(await userService.GetUserByNameAsync(name, userResolverService.GetUserId()));
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            return this.ConvertResult(await userService.GetUserByIdAsync(id, userResolverService.GetUserId()));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsersAsync([FromQuery] UserSearchPaginationModel userSearchModel)
        {
            return this.ConvertResult(await userService.GetUsersWithNameAsync(userSearchModel));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateModel userUpdateModel)
        {
            return this.ConvertResult(
                await userService.UpdateUserAsync(userUpdateModel, userResolverService.GetUserId()));
        }

        [CredentialsFilter]
        [HttpPut("update/password")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] PasswordUpdateModel updatePasswordModel)
        {
            return this.ConvertResult(
                await userService.UpdatePasswordAsync(updatePasswordModel, userResolverService.GetUserId()));
        }
    }
}
