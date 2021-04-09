using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Models.TrainingTemplate;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/training-template")]
    public class TrainingTemplateController : ControllerBase
    {
        private readonly IUserResolverService userResolverService;
        private readonly ITrainingTemplateService trainingTemplateService;

        public TrainingTemplateController(
            IUserResolverService userResolverService,
            ITrainingTemplateService trainingTemplateService)
        {
            this.userResolverService = userResolverService;
            this.trainingTemplateService = trainingTemplateService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetTrainingTemplatesAsync([FromQuery] SortedPaginationModel model)
        {
            return Ok(await trainingTemplateService
                .GetTrainingTemplatesByUserIdAsync(model, userResolverService.GetUserId()));
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetTrainingTemplateAsync([FromQuery] int id)
        {
            return Ok(await trainingTemplateService.GetTrainingTemplateByIdAsync(id, userResolverService.GetUserId()));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTrainigTemplateAsync([FromBody] TrainingTemplateModel model)
        {
            TrainingTemplateDto trainingTemplateDto =
                await trainingTemplateService.AddTrainingTemplateAsync(model, userResolverService.GetUserId());
            
            if (trainingTemplateDto is not null)
                return Ok(trainingTemplateDto);
            return BadRequest();
        }

        [HttpPost("clone")]
        public async Task<IActionResult> CloneTrainigTemplateAsync([FromBody] int templateId)
        {
            TrainingTemplateDto trainingTemplateDto =
                await trainingTemplateService.CloneForCreatorAsync(templateId, userResolverService.GetUserId());

            if (trainingTemplateDto is not null)
                return Ok(trainingTemplateDto);
            return BadRequest();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTrainingTemplateAsync([FromBody] TrainingTemplateUpdateModel model)
        {
            TrainingTemplateDto trainingTemplateDto =
                await trainingTemplateService.UpdateTrainingTemplateAsync(model, userResolverService.GetUserId());

            if (trainingTemplateDto is not null)
                return Ok(trainingTemplateDto);
            return BadRequest();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteTrainingTemplateAsync([FromBody] int templateId)
        {
            bool result =
                await trainingTemplateService.DeleteTrainingTemplateAsync(templateId, userResolverService.GetUserId());

            if (result)
                return Ok();
            return BadRequest();
        }

    }
}
