using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.Exercise;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/exercise")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService exerciseService;
        private readonly IUserResolverService userResolverService;

        public ExerciseController(IExerciseService exerciseService, IUserResolverService userResolverService)
        {
            this.exerciseService = exerciseService;
            this.userResolverService = userResolverService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddExerciseAsync([FromBody] ExerciseModel exerciseModel)
        {
            ExerciseDto exerciseDto = 
                await exerciseService.AddExerciseAsync(exerciseModel, userResolverService.GetUserId());

            if (exerciseDto is null)
                return BadRequest();
            return Ok(exerciseDto);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateExerciseAsync([FromBody] ExerciseUpdateModel exerciseUpdateModel)
        {
            ExerciseDto exerciseDto =
                await exerciseService.UpdateExerciseAsync(exerciseUpdateModel, userResolverService.GetUserId());

            if (exerciseDto is null)
                return BadRequest();
            return Ok(exerciseDto);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveExerciseAsync([FromBody] int exerciseId)
        {
            bool result = await exerciseService.DeleteExerciseAsync(exerciseId, userResolverService.GetUserId());

            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
