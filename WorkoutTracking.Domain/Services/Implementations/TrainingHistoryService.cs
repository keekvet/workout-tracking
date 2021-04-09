using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Dto.TrainingHistory;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class TrainingHistoryService : ITrainingHistoryService
    {
        private readonly IMapper mapper;
        private readonly IRepository<TrainingHistory> trainingHistoryRepository;
        private readonly IRepository<TrainingTemplate> trainingTemplateRepository;
        private readonly IPaginationService<TrainingHistory, TrainingHistoryDto> paginationService;

        public TrainingHistoryService(
            IMapper mapper,
            IRepository<TrainingHistory> trainingHistoryRepository,
            IRepository<TrainingTemplate> trainingTemplateRepository,
            IPaginationService<TrainingHistory, TrainingHistoryDto> paginationService
            )
        {
            this.mapper = mapper;
            this.paginationService = paginationService;
            this.trainingHistoryRepository = trainingHistoryRepository;
            this.trainingTemplateRepository = trainingTemplateRepository;
        }

        public async Task<IEnumerable<TrainingHistoryDto>> GetAllTrainingHistoriesAsync(SortedPaginationModel model)
        {
            return await paginationService.GetRangeAsync(model);
        }

        public async Task<TrainingHistoryDto> AddTrainingHistoryAsync(int trainingTemplateId, int userId)
        {
            TrainingTemplate template = await trainingTemplateRepository.GetByIdAsync(trainingTemplateId);

            if (template?.CreatorId != userId)
                return null;

            TrainingHistory trainingHistory = mapper.Map<TrainingTemplate, TrainingHistory>(template);
            trainingHistory.Start = DateTime.Now.ToUniversalTime();

            trainingHistory = await trainingHistoryRepository.AddAsync(trainingHistory);
            await trainingHistoryRepository.SaveChangesAsync();

            return mapper.Map<TrainingHistory, TrainingHistoryDto>(trainingHistory);
        }
    }
}
