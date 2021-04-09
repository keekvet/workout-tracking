using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.TrainingHistory;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IExerciseHistoryService
    {
        Task<ExerciseHistoryDto> AddExerciseHistoryAsync(int exerciseId, int userId);
    }
}
