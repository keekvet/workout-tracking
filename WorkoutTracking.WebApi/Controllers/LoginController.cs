using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout_tracking.Filters;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Application.Models.User;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Application.Dto.User;

namespace Workout_tracking.Controllers
{
    [ApiController]
    [Route("api/login/")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        [CredentialsFilter]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            UserTokenDto result = await loginService.LoginAsync(user);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }
    }
}
