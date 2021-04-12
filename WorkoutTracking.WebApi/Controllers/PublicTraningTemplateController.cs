using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Services.Interfaces;

namespace Workout_tracking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/public-template")]
    public class PublicTraningTemplateController : ControllerBase
    {
        private readonly IUserResolverService userResolverService;
        private readonly IPublicTrainingTemplateService publicTraningService;

        public PublicTraningTemplateController(
            IUserResolverService userResolverService, 
            IPublicTrainingTemplateService publicTraningService)
        {
            this.userResolverService = userResolverService;
            this.publicTraningService = publicTraningService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPublicTemplateAsync([FromBody] int templateId)
        {
            PublicTrainingTemplateDto templateDto = 
                await publicTraningService.AddPublicTemplateAsync(templateId, userResolverService.GetUserId());

            if (templateDto is not null)
                return Ok(templateDto);
            return BadRequest();
        }

        [HttpPost("clone")]
        public async Task<IActionResult> ClonePublicTemplateAsync([FromBody] int templateId)
        {
            TrainingTemplateDto templateDto =
                await publicTraningService.ClonePublicTemplateAsync(templateId, userResolverService.GetUserId());

            if (templateDto is not null)
                return Ok(templateDto);
            return BadRequest();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeletePublicTemplateAsync([FromBody] int templateId)
        {
            bool result = await publicTraningService.DeletePublicTemplateAsync(
                templateId, 
                userResolverService.GetUserId());

            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPublicTemplatesAsync(
            [FromQuery] PublicTrainingTemplatePaginationModel model)
        {
            IEnumerable<PublicTrainingTemplateDto> result =
                await publicTraningService.GetPublicTemplatesAsync(model);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPublicTemplates(
            [FromQuery] PublicTrainingTemplatePaginationModel model, 
            int userId)
        {
            IEnumerable<PublicTrainingTemplateDto> result =
                await publicTraningService.GetPublicTemplatesAsync(model, userId);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }


    }
}
