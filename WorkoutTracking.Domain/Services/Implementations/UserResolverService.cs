using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Services.Interfaces;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class UserResolverService : IUserResolverService
    {
        IHttpContextAccessor context;

        public UserResolverService(IHttpContextAccessor context)
        {
            this.context = context;
        }

        public int GetUserId()
        {
            return int.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
