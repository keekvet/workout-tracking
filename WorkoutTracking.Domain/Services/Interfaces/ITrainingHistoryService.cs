using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.TrainingHistory;
using WorkoutTracking.Application.Models.Pagination.Base;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface ITrainingHistoryService
    {
        Task<TrainingHistoryDto> AddTrainingHistoryAsync(int trainingTemplateId, int userId);
        Task<IEnumerable<TrainingHistoryDto>> GetAllTrainingHistoriesAsync(SortedPaginationModel model);
    }
}
