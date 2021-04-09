using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Models.TrainingTemplate;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface ITrainingTemplateService
    {
        Task<TrainingTemplateDto> AddTrainingTemplateAsync(TrainingTemplateModel model, int userId);
        Task<TrainingTemplateDto> CloneAsync(TrainingTemplate template, int userId);
        Task<TrainingTemplateDto> CloneForCreatorAsync(int templateId, int userId);
        Task<bool> DeleteTrainingTemplateAsync(int templateId, int userId);
        Task<TrainingTemplateDto> GetTrainingTemplateByIdAsync(int templateId, int userId);
        Task<IEnumerable<TrainingTemplateDto>> GetTrainingTemplatesByUserIdAsync(
            SortedPaginationModel model, 
            int userId);
        Task<TrainingTemplateDto> UpdateTrainingTemplateAsync(TrainingTemplateUpdateModel model, int userId);
    }
}
