using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/training-history")]
    public class TrainingHistoryController : ControllerBase
    {
        private readonly IUserResolverService userResolverService;
        private readonly ITrainingHistoryService trainingHistoryService;

        public TrainingHistoryController(
            IUserResolverService userResolverService, 
            ITrainingHistoryService trainingHistoryService)
        {
            this.userResolverService = userResolverService;
            this.trainingHistoryService = trainingHistoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetHistory([FromQuery]SortedPaginationModel model)
        {
            return Ok(await trainingHistoryService.GetAllTrainingHistoriesAsync(model));
        }
    }
}
