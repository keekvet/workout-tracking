using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.ExerciseProperty;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IExercisePropertyService
    {
        Task<ExercisePropertyDto> AddPropertyAsync(ExercisePropertyModel model, int userId);
        Task<bool> DeletePropertyAsync(int propertyId, int userId);
        Task<ExercisePropertyDto> UpdatePropertyAsync(ExercisePropertyUpdateModel model, int userId);
    }
}
