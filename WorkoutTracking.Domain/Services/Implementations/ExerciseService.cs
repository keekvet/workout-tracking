using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Extensions;
using WorkoutTracking.Application.Models.Exercise;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class ExerciseService : IExerciseService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Exercise> exerciseRepository;
        private readonly IRepository<TrainingTemplate> trainingTemplateRepository;

        public ExerciseService(
            IMapper mapper,
            IRepository<Exercise> exerciseRepository,
            IRepository<TrainingTemplate> trainingTemplateRepository)
        {
            this.mapper = mapper;
            this.exerciseRepository = exerciseRepository;
            this.trainingTemplateRepository = trainingTemplateRepository;
        }

        public async Task<ExerciseDto> UpdateExerciseAsync(ExerciseUpdateModel model, int userId)
        {
            return await UpsertExerciseAsync(mapper.Map<ExerciseUpdateModel, Exercise>(model), userId);
        }

        public async Task<ExerciseDto> AddExerciseAsync(ExerciseModel model, int userId)
        {
            return await UpsertExerciseAsync(mapper.Map<ExerciseModel, Exercise>(model), userId);
        }

        public async Task<bool> DeleteExerciseAsync(int exerciseId, int userId)
        {
            Exercise exercise = await exerciseRepository.GetByIdAsync(exerciseId);

            int? creatorId = exercise?.TrainingTemplate?.CreatorId;

            if (creatorId != userId)
                return false;

            bool result = await exerciseRepository.DeleteAsync(exercise);
            await exerciseRepository.SaveChangesAsync();

            IEnumerable<Exercise> exercises = exercise.TrainingTemplate.Exercises;

            OrderExercisesByPosition(ref exercises);

            foreach (Exercise item in exercises)
                await exerciseRepository.UpdateAsync(item);

            await exerciseRepository.SaveChangesAsync();
                
            return result;
        }
        
        private async Task<ExerciseDto> UpsertExerciseAsync(Exercise exercise, int userId)
        {
            if (exercise is null)
                return null;

            TrainingTemplate trainingTemplate =
                await trainingTemplateRepository.GetByIdAsync(exercise.TrainingTemplateId) ??
                (await exerciseRepository.GetByIdAsync(exercise.Id)).TrainingTemplate;

            if (trainingTemplate is null || trainingTemplate.CreatorId != userId)
                return null;

            IEnumerable<Exercise> exercises = trainingTemplate.Exercises;

            exercises = exercises.Where(e => e.Id != exercise.Id).ToList();

            ReorderExercises(ref exercises, ref exercise);

            foreach (Exercise item in exercises)
                await exerciseRepository.UpdateAsync(item);
            
            Exercise upsertedExercise;

            if (exercises.Count() != trainingTemplate.Exercises.Count())
            {
                Exercise exerciseToUpdate = await exerciseRepository.GetByIdAsync(exercise.Id);
                exerciseToUpdate.Copy(exercise);
                upsertedExercise = await exerciseRepository.UpdateAsync(exerciseToUpdate);
            }
            else
                upsertedExercise = await exerciseRepository.AddAsync(exercise);

            await exerciseRepository.SaveChangesAsync();
            return mapper.Map<Exercise, ExerciseDto>(upsertedExercise);

        }


        private void ReorderExercises(ref IEnumerable<Exercise> exercises, ref Exercise newExercise)
        {
            OrderExercisesByPosition(ref exercises);

            if (exercises is null || newExercise is null)
                return;

            if(exercises.Count() == 0)
            {
                newExercise.Position = 1;
                return;
            }    

            IEnumerable<int> positionRange = Enumerable.Range(1, exercises.Count());

            if (positionRange.Contains(newExercise.Position))
                exercises = exercises.Skip(newExercise.Position - 1).Select(e => 
                {
                    e.Position += 1;
                    return e;
                });
            else
                newExercise.Position = exercises.Max(e => e.Position) + 1;
        }

        private void OrderExercisesByPosition(ref IEnumerable<Exercise> exercises)
        {
            if (exercises is null)
                return;

            IEnumerable<int> positions = Enumerable.Range(1, exercises.Count());
            
            exercises = 
                exercises
                .OrderBy(e => e.Position)
                .Zip(positions)
                .Select(e =>
                {
                    e.First.Position = e.Second;
                    return e.First;
                });
        }
    }
}
