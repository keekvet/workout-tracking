using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.TrainingTemplate;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [ApiController]
    [Route("api/training-template")]
    public class TrainingTemplateController : ControllerBase
    {
        IUserResolverService userResolverService;
        ITrainingTemplateService trainingTemplateService;

        public TrainingTemplateController(
            IUserResolverService userResolverService,
            ITrainingTemplateService trainingTemplateService)
        {
            this.userResolverService = userResolverService;
            this.trainingTemplateService = trainingTemplateService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetTrainingTemplates()
        {
            return Ok(await trainingTemplateService.GetTrainingTemplatesByUserIdAsync(userResolverService.GetUserId()));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTrainigTemplate([FromBody] TrainingTemplateModel model)
        {
            TrainingTemplateDto trainingTemplateDto =
                await trainingTemplateService.AddTrainingTemplateAsync(model, userResolverService.GetUserId());
            
            if (trainingTemplateDto is not null)
                return Ok(trainingTemplateDto);
            return BadRequest();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTrainingTemplate([FromBody] TrainingTemplateUpdateModel model)
        {
            TrainingTemplateDto trainingTemplateDto =
                await trainingTemplateService.UpdateTrainingTemplateAsync(model, userResolverService.GetUserId());

            if (trainingTemplateDto is not null)
                return Ok(trainingTemplateDto);
            return BadRequest();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteTrainingTemplate([FromBody] int templateId)
        {
            bool result =
                await trainingTemplateService.DeleteTrainingTemplateAsync(templateId, userResolverService.GetUserId());

            if (result)
                return Ok();
            return BadRequest();
        }

    }
}
