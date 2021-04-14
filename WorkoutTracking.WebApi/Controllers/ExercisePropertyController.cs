using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExercisePropertyAsync(int id)
        {
            return this.ConvertResult(
                await exercisePropertyService.GetExercisePropertyAsync(id, userResolverService.GetUserId()));

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddExercisePropertyAsync([FromBody] ExercisePropertyModel exerciseModel)
        {
            return this.ConvertResult(
                await exercisePropertyService.AddPropertyAsync(exerciseModel, userResolverService.GetUserId()));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateExercisePropertyAsync([FromBody] ExercisePropertyUpdateModel exerciseUpdateModel)
        {
            return this.ConvertResult(
                await exercisePropertyService.UpdatePropertyAsync(exerciseUpdateModel, userResolverService.GetUserId()));
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveExercisePropertyAsync([FromBody] int exercisePropertyId)
        {
            return this.ConvertResult(await exercisePropertyService.DeletePropertyAsync(exercisePropertyId, userResolverService.GetUserId()));
        }
    }
}
