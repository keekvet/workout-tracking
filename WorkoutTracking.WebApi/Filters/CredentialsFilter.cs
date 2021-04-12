using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Workout_tracking.Filters
{
    public class CredentialsFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            
            if(exception is SecurityTokenException)
                context.HttpContext.Response.StatusCode = 400;
            else if(exception is InvalidCredentialException)
                context.HttpContext.Response.StatusCode = 401;

            context.Result = new ObjectResult(new { Message = exception.Message });
        }
    }
}
