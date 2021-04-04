using AutoMapper;
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
        IMapper mapper;
        IRepository<TrainingCategory> trainingRepository;
        IPaginationService<TrainingCategory, TrainingCategoryDto> paginationService;

        public TrainingCategoryService(
            IMapper mapper,
            IRepository<TrainingCategory> trainingRepository,
            IPaginationService<TrainingCategory, TrainingCategoryDto> paginationService)
        {
            this.mapper = mapper;
            this.paginationService = paginationService;
            this.trainingRepository = trainingRepository;
        }

        public async Task<IEnumerable<TrainingCategoryDto>> GetAllAsync(SortedPaginationModel model)
        {
            return await paginationService.GetRangeAsync(model);
        }

        public async Task<TrainingCategoryDto> GetCategoryById(int id)
        {
            return mapper.Map<TrainingCategory, TrainingCategoryDto>(await trainingRepository.GetByIdAsync(id));
        }
    }
}
