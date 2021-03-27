using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout_tracking.Filters;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Domain.Models.User;
using WorkoutTracking.Domain.Services.Interfaces;

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
        [LoginFilter]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            return Ok(await loginService.LoginAsync(user));
        }
    }
}
