using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.Exercise;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<ExerciseDto> AddExerciseAsync(ExerciseModel model, int userId);
        Task<bool> DeleteExerciseAsync(int exerciseId, int userId);
        Task<ExerciseDto> UpdateExerciseAsync(ExerciseUpdateModel model, int userId);
    }
}
