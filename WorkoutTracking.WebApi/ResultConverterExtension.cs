using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.Error;

namespace Workout_tracking
{
    public static class ResultConverterExtension
    {
        public static IActionResult ConvertResult<T>(
            this ControllerBase controller, T result, string errorMessage = null) where T : class
        {
            if (result is null)
                return controller.BadRequest(new ErrorMessageDto { Message = errorMessage });
            return controller.Ok(result);
        }

        public static IActionResult ConvertResult(
            this ControllerBase controller, bool result, string errorMessage = null)
        {
            if (result is false)
                return controller.BadRequest(new ErrorMessageDto { Message = errorMessage });
            return controller.Ok(result);
        }
    }
}
