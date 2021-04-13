using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Extensions;
using WorkoutTracking.Application.Models.ExerciseProperty;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class ExercisePropertyService : IExercisePropertyService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Exercise> exerciseRepository;
        private readonly IRepository<ExerciseProperty> exercisePropertyRepository;

        public ExercisePropertyService(
            IMapper mapper,
            IRepository<Exercise> exerciseRepository,
            IRepository<ExerciseProperty> exercisePropertyRepository)
        {
            this.mapper = mapper;
            this.exerciseRepository = exerciseRepository;
            this.exercisePropertyRepository = exercisePropertyRepository;
        }
        private async Task<ExercisePropertyDto> UpsertPropertyAsync(ExerciseProperty exerciseProperty, int userId)
        {
            if (exerciseProperty is null)
                return null;

            Exercise exercise =
                await exerciseRepository.GetByIdAsync(exerciseProperty.ExerciseId) ??
                (await exercisePropertyRepository.GetByIdAsync(exerciseProperty.Id)).Exercise;

            if (exercise.TrainingTemplate.CreatorId != userId)
                return null;

            if (exerciseProperty.Id != 0)
            {
                exerciseProperty =
                    exercise.Properties.FirstOrDefault(p => p.Id == exerciseProperty.Id).Copy(exerciseProperty);

                exerciseProperty = await exercisePropertyRepository.UpdateAsync(exerciseProperty);
            }
            else
                exerciseProperty = await exercisePropertyRepository.AddAsync(exerciseProperty);

            await exercisePropertyRepository.SaveChangesAsync();

            return mapper.Map<ExerciseProperty, ExercisePropertyDto>(exerciseProperty);
        }

        public async Task<ExercisePropertyDto> GetExercisePropertyAsync(int id, int userId)
        {
            ExerciseProperty property = await exercisePropertyRepository.GetByIdAsync(id);

            if (property?.Exercise.TrainingTemplate.CreatorId != userId)
                return null;

            return mapper.Map<ExerciseProperty, ExercisePropertyDto>(property);
        }


        public async Task<ExercisePropertyDto> AddPropertyAsync(ExercisePropertyModel model, int userId)
        {
            ExerciseProperty exerciseProperty = mapper.Map<ExercisePropertyModel, ExerciseProperty>(model);
            
            return await UpsertPropertyAsync(exerciseProperty, userId);
        }

        public async Task<ExercisePropertyDto> UpdatePropertyAsync(ExercisePropertyUpdateModel model, int userId)
        {
            ExerciseProperty exerciseProperty = mapper.Map<ExercisePropertyUpdateModel, ExerciseProperty>(model);
         
            return await UpsertPropertyAsync(exerciseProperty, userId);
        }

        public async Task<bool> DeletePropertyAsync(int propertyId, int userId)
        {
            ExerciseProperty exerciseProperty = await exercisePropertyRepository.GetByIdAsync(propertyId);

            if (exerciseProperty?.Exercise.TrainingTemplate.CreatorId != userId)
                return false;

            bool result = await exercisePropertyRepository.DeleteAsync(exerciseProperty);
            await exercisePropertyRepository.SaveChangesAsync();

            return result;
        }
    }
}
