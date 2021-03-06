using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/training-category")]
    public class TrainingCategoryController : ControllerBase
    {
        private readonly ITrainingCategoryService trainingCategoryService;

        public TrainingCategoryController(ITrainingCategoryService trainingCategoryService)
        {
            this.trainingCategoryService = trainingCategoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetCategoriesAll([FromQuery] SortedPaginationModel model)
        {
            return this.ConvertResult(await trainingCategoryService.GetAllAsync(model));
        }
    }
}
