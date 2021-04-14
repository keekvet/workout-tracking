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
            return this.ConvertResult(
                await publicTraningService.AddPublicTemplateAsync(templateId, userResolverService.GetUserId()));
        }

        [HttpPost("clone")]
        public async Task<IActionResult> ClonePublicTemplateAsync([FromBody] int templateId)
        {
            return this.ConvertResult(
                await publicTraningService.ClonePublicTemplateAsync(templateId, userResolverService.GetUserId()));
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeletePublicTemplateAsync([FromBody] int templateId)
        {
            return this.ConvertResult(
                await publicTraningService.DeletePublicTemplateAsync(templateId, userResolverService.GetUserId()));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPublicTemplatesAsync(
            [FromQuery] PublicTrainingTemplatePaginationModel model)
        {
            return this.ConvertResult(await publicTraningService.GetPublicTemplatesAsync(model));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPublicTemplatesAsync(
            [FromQuery] PublicTrainingTemplatePaginationModel model, 
            int userId)
        {
            return this.ConvertResult(await publicTraningService.GetPublicTemplatesAsync(model, userId));
        }


    }
}
