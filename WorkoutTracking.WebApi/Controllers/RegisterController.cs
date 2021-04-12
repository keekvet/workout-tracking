using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Application.Models.User;
using WorkoutTracking.Application.Services.Interfaces;
using Workout_tracking.Filters;
using WorkoutTracking.Application.Dto.User;

namespace Workout_tracking.Controllers
{
    [ApiController]
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService registerService;

        public RegisterController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }

        [HttpPost]
        [CredentialsFilter]
        public async Task<IActionResult> Register(UserRegisterModel user)
        {
            UserDto userDto = await registerService.Register(user);
            if (userDto is null)
                return BadRequest();
            return Ok(userDto);
        }
    }
}
