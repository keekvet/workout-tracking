using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/active-training")]
    public class ActiveTrainingController : ControllerBase
    {
        private readonly IUserResolverService userResolverService;
        private readonly IActiveTrainingService activeTrainingService;

        public ActiveTrainingController(
            IUserResolverService userResolverService, 
            IActiveTrainingService activeTrainingService)
        {
            this.userResolverService = userResolverService;
            this.activeTrainingService = activeTrainingService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetActiveTrainingAsync()
        {
            return this.ConvertResult(await activeTrainingService.GetActiveTrainingAsync(userResolverService.GetUserId()));
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartTrainingAsync([FromBody] int trainingTemplateId)
        {
            return this.ConvertResult(
                await activeTrainingService.StartTrainingAsync(trainingTemplateId, userResolverService.GetUserId()));
        }

        [HttpGet("exercise")]
        public async Task<IActionResult> GetExerciseAsync()
        {
            return this.ConvertResult(await activeTrainingService.GetExerciseAsync(userResolverService.GetUserId()));
        }

        [HttpPost("perform-exercise")]
        public async Task<IActionResult> PeformExerciseAsync()
        {
            return this.ConvertResult(await activeTrainingService.PeformExerciseAsync(userResolverService.GetUserId()));
        }

        [HttpDelete("end")]
        public async Task<IActionResult> EndTraininAsync()
        {
            return this.ConvertResult(await activeTrainingService.EndTrainingAsync(userResolverService.GetUserId()));
        }

    }
}
