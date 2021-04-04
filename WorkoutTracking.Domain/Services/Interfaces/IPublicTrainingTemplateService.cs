using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.Pagination.Base;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IPublicTrainingTemplateService
    {
        Task<IEnumerable<PublicTrainingTemplateDto>> GetPublicTemplatesAsync(SortedPaginationModel model);
    }
}
