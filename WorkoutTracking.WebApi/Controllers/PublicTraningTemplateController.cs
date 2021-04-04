using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<IActionResult> AddPublicTemplate([FromBody] int templateId)
        {
            throw new NotImplementedException();
        }
    }
}
