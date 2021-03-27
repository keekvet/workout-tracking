using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Domain.Models.User;
using WorkoutTracking.Domain.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [ApiController]
    [Route("api/register/")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService registerService;

        public RegisterController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel user)
        {
            return Ok(await registerService.Register(user));
        }
    }
}
