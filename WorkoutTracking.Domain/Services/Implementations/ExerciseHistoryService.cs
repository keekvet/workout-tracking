using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.TrainingHistory;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class ExerciseHistoryService : IExerciseHistoryService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Exercise> exerciseRepository;
        private readonly IRepository<ExerciseHistory> exerciseHistoryRepository;
        private readonly IRepository<ExercisePropertyHistory> propertyHistoryRepository;

        public ExerciseHistoryService(
            IMapper mapper, 
            IRepository<Exercise> exerciseRepository,
            IRepository<ExerciseHistory> exerciseHistoryRepository, 
            IRepository<ExercisePropertyHistory> propertyHistoryRepository)
        {
            this.mapper = mapper;
            this.exerciseRepository = exerciseRepository;
            this.exerciseHistoryRepository = exerciseHistoryRepository;
            this.propertyHistoryRepository = propertyHistoryRepository;
        }

        public async Task<ExerciseHistoryDto> AddExerciseHistoryAsync(int exerciseId, int userId)
        {
            Exercise exercise = await exerciseRepository.GetByIdAsync(exerciseId);

            if (exercise?.TrainingTemplate.CreatorId != userId)
                return null;

            ActiveTraining activeTraining = exercise.TrainingTemplate.Creator.ActiveTraining;

            if (activeTraining is null || !exercise.TrainingTemplate.Equals(activeTraining.TrainingTemplate))
                return null;

            ExerciseHistory exerciseHistory = mapper.Map<Exercise, ExerciseHistory>(exercise);
            exerciseHistory.EndDate = DateTime.Now.ToUniversalTime();
            exerciseHistory.TrainingHistoryId = activeTraining.TrainingHistory.Id;
            exerciseHistory.Properties =
                exercise.Properties.Select(p => mapper.Map<ExerciseProperty, ExercisePropertyHistory>(p)).ToList();


            exerciseHistory = await exerciseHistoryRepository.AddAsync(exerciseHistory);
            await exerciseHistoryRepository.SaveChangesAsync();

            return mapper.Map<ExerciseHistory, ExerciseHistoryDto>(exerciseHistory);
        }
    }
}
