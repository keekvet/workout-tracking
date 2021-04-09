using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Models.ScheduledTraining;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/training-schedule")]
    public class TrainingScheduleController : ControllerBase
    {
        private readonly IUserResolverService userResolverService;
        private readonly IScheduledTrainingService scheduledTrainingService;

        public TrainingScheduleController(
            IUserResolverService userResolverService, 
            IScheduledTrainingService scheduledTrainingService)
        {
            this.userResolverService = userResolverService;
            this.scheduledTrainingService = scheduledTrainingService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTrainingSchedule([FromBody] ScheduledTrainingModel model)
        {
            ScheduledTrainingDto scheduledTraining =
                await scheduledTrainingService.AddScheduledTraining(model, userResolverService.GetUserId());

            if (scheduledTraining is null)
                return BadRequest();

            return Ok(scheduledTraining);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteTrainingScheduleById([FromBody] int trainingScheduleId)
        {
            bool result = await scheduledTrainingService.DeleteScheduledTrainingById(
                trainingScheduleId, userResolverService.GetUserId());
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTrainingSchedules([FromQuery]SortedPaginationModel model)
        {
            return Ok(await scheduledTrainingService.GetAllScheduledTrainins(model, userResolverService.GetUserId()));
        }

        [HttpGet("id/{trainingId}")]
        public async Task<IActionResult> GetTrainingScheduleById(int trainingId)
        {
            ScheduledTrainingDto scheduledTraining =
                await scheduledTrainingService.GetScheduledTrainingById(trainingId, userResolverService.GetUserId());

            if (scheduledTraining is null)
                return BadRequest();

            return Ok(scheduledTraining);
        }
    }
}
