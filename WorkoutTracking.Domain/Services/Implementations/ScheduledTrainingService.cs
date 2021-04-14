using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Models.ScheduledTraining;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class ScheduledTrainingService : IScheduledTrainingService
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly ITrainingTemplateService trainingTemplateService;
        private readonly IRepository<ScheduledTraining> scheduledTrainingRepository;
        private readonly IPaginationService<ScheduledTraining, ScheduledTrainingDto> paginationService;
        public ScheduledTrainingService(
            IMapper mapper,
            IUserService userService,
            ITrainingTemplateService trainingTemplateService,
            IRepository<ScheduledTraining> scheduledTrainingRepository,
            IPaginationService<ScheduledTraining, ScheduledTrainingDto> paginationService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.paginationService = paginationService;
            this.trainingTemplateService = trainingTemplateService;
            this.scheduledTrainingRepository = scheduledTrainingRepository;
        }

        public async Task<IEnumerable<ScheduledTrainingDto>> GetAllScheduledTrainins(
            SortedPaginationModel model,
            int userId)
        {
            Expression<Func<ScheduledTraining, bool>> filter = s => s.Template.CreatorId == userId;

            return await paginationService.GetRangeAsync(model, filter);
        }

        public async Task<ScheduledTrainingDto> GetScheduledTrainingById(int id, int userId)
        {
            ScheduledTraining result = await scheduledTrainingRepository.GetByIdAsync(id);
            if (result?.Template.CreatorId != userId)
                return null;

            return mapper.Map<ScheduledTraining, ScheduledTrainingDto>(result);
        }

        public async Task<ScheduledTrainingDto> AddScheduledTraining(ScheduledTrainingModel model, int userId)
        {
            TrainingTemplateDto trainingTemplate = 
                await trainingTemplateService.GetTrainingTemplateByIdAsync(model.TemplateId, userId);

            if (trainingTemplate is null)
                return null;

            ScheduledTraining scheduledTraining = mapper.Map<ScheduledTrainingModel, ScheduledTraining>(model);
            scheduledTraining.TrainingTemplateId = model.TemplateId;
            
            scheduledTraining = await scheduledTrainingRepository.AddAsync(scheduledTraining);

            await scheduledTrainingRepository.SaveChangesAsync();
            return mapper.Map<ScheduledTraining, ScheduledTrainingDto>(scheduledTraining);
        }

        public async Task<bool> DeleteScheduledTrainingById(int id, int userId)
        {
            ScheduledTraining scheduledTraining = await scheduledTrainingRepository.GetByIdAsync(id);
            if (scheduledTraining?.Template.CreatorId != userId)
                return false;

            bool result = await scheduledTrainingRepository.DeleteAsync(scheduledTraining);
            await scheduledTrainingRepository.SaveChangesAsync();

            return result;
        }
    }
}
