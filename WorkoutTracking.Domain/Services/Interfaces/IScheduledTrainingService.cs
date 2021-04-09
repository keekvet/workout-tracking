using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Models.ScheduledTraining;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IScheduledTrainingService
    {
        Task<ScheduledTrainingDto> AddScheduledTraining(ScheduledTrainingModel model, int userId);
        Task<bool> DeleteScheduledTrainingById(int id, int userId);
        Task<IEnumerable<ScheduledTrainingDto>> GetAllScheduledTrainins(SortedPaginationModel model, int userId);
        Task<ScheduledTrainingDto> GetScheduledTrainingById(int id, int userId);
    }
}
