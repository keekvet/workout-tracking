using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Dto.TrainingExtra;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IActiveTrainingService
    {
        Task<bool> EndTrainingAsync(int userId);
        Task<ExerciseDto> PeformExerciseAsync(int userId);
        Task<ActiveTrainingDto> StartTrainingAsync(int trainingTemplateId, int userId);
        Task<ExerciseDto> GetExerciseAsync(int userId);
        Task<ActiveTrainingDto> GetActiveTrainingAsync(int userId);
    }
}
