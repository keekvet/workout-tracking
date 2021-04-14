using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseAsync(int id)
        {
            return this.ConvertResult(await exerciseService.GetExerciseAsync(id, userResolverService.GetUserId()));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddExerciseAsync([FromBody] ExerciseModel exerciseModel)
        {
            return this.ConvertResult(
                await exerciseService.AddExerciseAsync(exerciseModel, userResolverService.GetUserId()));

        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateExerciseAsync([FromBody] ExerciseUpdateModel exerciseUpdateModel)
        {
            return this.ConvertResult(
                await exerciseService.UpdateExerciseAsync(exerciseUpdateModel, userResolverService.GetUserId()));
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveExerciseAsync([FromBody] int exerciseId)
        {
            return this.ConvertResult(
                await exerciseService.DeleteExerciseAsync(exerciseId, userResolverService.GetUserId()));
        }
    }
}
