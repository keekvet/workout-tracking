using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{

    [ApiController]
    [Route("api/training-history")]
    public class TrainingHistoryContorller : ControllerBase
    {
        private readonly IUserResolverService userResolverService;
        private readonly ITrainingHistoryService trainingHistoryService;

        public TrainingHistoryContorller(
            IUserResolverService userResolverService, 
            ITrainingHistoryService trainingHistoryService)
        {
            this.userResolverService = userResolverService;
            this.trainingHistoryService = trainingHistoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetHistory()
        {
            throw new NotImplementedException();
        }

        [HttpGet("id/{historyId}")]
        public async Task<IActionResult> GetHistoryById(int historyId)
        {
            throw new NotImplementedException();
        }
    }
}
