using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class TrainingCategoryService : ITrainingCategoryService
    {
        IPaginationService<TrainingCategory, TrainingCategoryDto> paginationService;

        public TrainingCategoryService(
            IPaginationService<TrainingCategory, TrainingCategoryDto> paginationService)
        {
            this.paginationService = paginationService;
        }

        public async Task<IEnumerable<TrainingCategoryDto>> GetAllAsync(SortedPaginationModel model)
        {
            return await paginationService.GetRangeAsync(model);
        }
    }
}
