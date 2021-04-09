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
            ActiveTrainingDto activeTraining =
                await activeTrainingService.GetActiveTrainingAsync(userResolverService.GetUserId());
            
            if (activeTraining is null)
                return BadRequest();
            return Ok(activeTraining);
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartTrainingAsync([FromBody] int trainingTemplateId)
        {
            ActiveTrainingDto activeTraining =
                await activeTrainingService.StartTrainingAsync(trainingTemplateId, userResolverService.GetUserId());

            if (activeTraining is null)
                return BadRequest();
            return Ok(activeTraining);
        }

        [HttpGet("next-exercise")]
        public async Task<IActionResult> GetExerciseAsync()
        {
            ExerciseDto exercise= await activeTrainingService.GetExerciseAsync(userResolverService.GetUserId());
            if (exercise is null)
                return BadRequest();
            return Ok(exercise);
        }

        [HttpPost("perform-exercise")]
        public async Task<IActionResult> PeformExerciseAsync()
        {
            ExerciseDto exerciseDto = await activeTrainingService.PeformExerciseAsync(userResolverService.GetUserId());
            
            if (exerciseDto is null)
                return BadRequest();
            return Ok(exerciseDto);
        }

        [HttpDelete("end")]
        public async Task<IActionResult> EndTraininAsync()
        {
            bool result = await activeTrainingService.EndTrainingAsync(userResolverService.GetUserId());

            if (result)
                return Ok();
            return BadRequest();
        }

    }
}
