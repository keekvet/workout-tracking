using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Models.Pagination.Base;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IPublicTrainingTemplateService
    {
        Task<PublicTrainingTemplateDto> AddPublicTemplateAsync(int templateId, int userId);
        Task<TrainingTemplateDto> ClonePublicTemplateAsync(int publicTemplateId, int userId);
        Task<bool> DeletePublicTemplateAsync(int templateId, int userId);
        Task<IEnumerable<PublicTrainingTemplateDto>> GetPublicTemplatesAsync(
            PublicTrainingTemplatePaginationModel model,
            int? userId = null);
    }
}
