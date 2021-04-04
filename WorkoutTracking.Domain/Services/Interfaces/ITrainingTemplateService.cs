using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.TrainingTemplate;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface ITrainingTemplateService
    {
        Task<TrainingTemplateDto> AddTrainingTemplateAsync(TrainingTemplateModel model, int userId);
        Task<bool> DeleteTrainingTemplateAsync(int templateId, int userId);
        Task<IEnumerable<TrainingTemplateDto>> GetTrainingTemplatesByUserIdAsync(int userId);
        Task<TrainingTemplateDto> UpdateTrainingTemplateAsync(TrainingTemplateUpdateModel model, int userId);
    }
}
