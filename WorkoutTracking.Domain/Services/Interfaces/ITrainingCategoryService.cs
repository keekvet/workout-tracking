using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Application.Models.Pagination.Base;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface ITrainingCategoryService
    {
        Task<IEnumerable<TrainingCategoryDto>> GetAllAsync(SortedPaginationModel model);
        Task<TrainingCategoryDto> GetCategoryByIdAsync(int id);
    }
}
