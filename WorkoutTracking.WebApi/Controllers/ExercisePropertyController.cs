using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.ExerciseProperty;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/exercise-property")]
    public class ExercisePropertyController : ControllerBase
    {
        private readonly IUserResolverService userResolverService;
        private readonly IExercisePropertyService exercisePropertyService;

        public ExercisePropertyController(
            IUserResolverService userResolverService, 
            IExercisePropertyService exercisePropertyService)
        {
            this.userResolverService = userResolverService;
            this.exercisePropertyService = exercisePropertyService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddExercisePropertyAsync([FromBody] ExercisePropertyModel exerciseModel)
        {
            ExercisePropertyDto exercisePropertyDto =
                await exercisePropertyService.AddPropertyAsync(exerciseModel, userResolverService.GetUserId());

            if (exercisePropertyDto is null)
                return BadRequest();
            return Ok(exercisePropertyDto);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateExercisePropertyAsync([FromBody] ExercisePropertyUpdateModel exerciseUpdateModel)
        {
            ExercisePropertyDto exerciseDto =
                await exercisePropertyService.UpdatePropertyAsync(exerciseUpdateModel, userResolverService.GetUserId());

            if (exerciseDto is null)
                return BadRequest();
            return Ok(exerciseDto);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveExercisePropertyAsync([FromBody] int exercisePropertyId)
        {
            bool result = 
                await exercisePropertyService.DeletePropertyAsync(exercisePropertyId, userResolverService.GetUserId());

            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
