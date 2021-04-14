using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Application.Dto.TrainingHistory;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class ActiveTrainingService : IActiveTrainingService
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> userRepository;
        private readonly ITrainingHistoryService trainingHistoryService;
        private readonly IExerciseHistoryService exerciseHistoryService;
        private readonly IRepository<ActiveTraining> activeTrainingRepository;
        private readonly IRepository<TrainingHistory> trainingHistoryRepository;
        private readonly IRepository<TrainingTemplate> trainingTemplateRepository;

        public ActiveTrainingService(
            IMapper mapper,
            IRepository<User> userRepository,
            ITrainingHistoryService trainingHistoryService,
            IExerciseHistoryService exerciseHistoryService,
            IRepository<ActiveTraining> activeTrainingRepository,
            IRepository<TrainingHistory> trainingHistoryRepository,
            IRepository<TrainingTemplate> trainingTemplateRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.trainingHistoryService = trainingHistoryService;
            this.exerciseHistoryService = exerciseHistoryService;
            this.activeTrainingRepository = activeTrainingRepository;
            this.trainingHistoryRepository = trainingHistoryRepository;
            this.trainingTemplateRepository = trainingTemplateRepository;
        }

        public async Task<ActiveTrainingDto> GetActiveTrainingAsync(int userId)
        {
            ActiveTraining activeTraining = (await userRepository.GetByIdAsync(userId)).ActiveTraining;

            if (activeTraining is null)
                return null;

            return mapper.Map<ActiveTraining, ActiveTrainingDto>(activeTraining);
        }

        public async Task<ActiveTrainingDto> StartTrainingAsync(int trainingTemplateId, int userId)
        {
            TrainingTemplate trainingTemplate = await trainingTemplateRepository.GetByIdAsync(trainingTemplateId);

            if (
                trainingTemplate?.CreatorId != userId 
                || trainingTemplate.Exercises.Count == 0
                || trainingTemplate.Creator.ActiveTraining is not null)
                return null;

            TrainingHistoryDto trainingHistoryDto =
                await trainingHistoryService.AddTrainingHistoryAsync(trainingTemplateId, userId);

            TrainingHistory trainingHistory = await trainingHistoryRepository.GetByIdAsync(trainingHistoryDto.Id);

            ActiveTraining activeTraining = new ActiveTraining()
            {
                UserId = trainingHistory.UserId,
                TrainingHistoryId = trainingHistory.Id,
                TrainingTemplateId = trainingTemplate.Id
            };

            activeTraining = await activeTrainingRepository.AddAsync(activeTraining);
            await activeTrainingRepository.SaveChangesAsync();

            return mapper.Map<ActiveTraining, ActiveTrainingDto>(activeTraining);
        }

        public async Task<ExerciseDto> GetExerciseAsync(int userId) 
        {
            ActiveTraining activeTraining = (await userRepository.GetByIdAsync(userId))?.ActiveTraining;

            if (activeTraining is null)
                return null;

            Exercise exercise = 
                activeTraining.TrainingTemplate.Exercises
                .FirstOrDefault(e => e.Position == activeTraining.ExerciseDonePosition + 1);
            
            if (exercise is null)
                return null;

            return mapper.Map<Exercise, ExerciseDto>(exercise);
        }

        public async Task<bool> EndTrainingAsync(int userId)
        {
            ActiveTraining activeTrainging = (await userRepository.GetByIdAsync(userId))?.ActiveTraining;

            if (activeTrainging is null)
                return false;

            bool result = await activeTrainingRepository.DeleteAsync(activeTrainging);
            await activeTrainingRepository.SaveChangesAsync();

            return result;
        }

        public async Task<ExerciseDto> PeformExerciseAsync(int userId)
        {
            ExerciseDto exerciseDto = await GetExerciseAsync(userId);

            if (exerciseDto is null)
                return null;

            ActiveTraining activeTraining = (await userRepository.GetByIdAsync(userId)).ActiveTraining;
            activeTraining.ExerciseDonePosition++;

            await activeTrainingRepository.UpdateAsync(activeTraining);

            ExerciseHistoryDto exerciseHistoryDto = 
                await exerciseHistoryService.AddExerciseHistoryAsync(exerciseDto.Id, userId);

            if (exerciseHistoryDto is null)
                return null;
            
            return exerciseDto;
        }
    }
}
