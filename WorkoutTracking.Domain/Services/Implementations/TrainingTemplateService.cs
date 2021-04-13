using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Extensions;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Models.TrainingTemplate;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class TrainingTemplateService : ITrainingTemplateService
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly ITrainingCategoryService trainingCategoryService;
        private readonly IRepository<TrainingTemplate> traingingTemplateRepository;
        private readonly IPaginationService<TrainingTemplate, TrainingTemplateDto> paginationService;

        public TrainingTemplateService(
            IMapper mapper,
            IUserService userService,
            ITrainingCategoryService trainingCategoryService,
            IRepository<TrainingTemplate> traingingTemplateRepository,
            IPaginationService<TrainingTemplate, TrainingTemplateDto> paginationService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.paginationService = paginationService;
            this.trainingCategoryService = trainingCategoryService;
            this.traingingTemplateRepository = traingingTemplateRepository;
        }

        private async Task<TrainingTemplateDto> UpsertTrainingTemplateAsync(TrainingTemplateUpdateModel model, int userId)
        {
            if (await trainingCategoryService.GetCategoryByIdAsync(model.CategoryId) is null)
                return null;

            TrainingTemplate trainingTemplate;

            if (model.Id.Equals(0))
            {
                trainingTemplate = mapper.Map<TrainingTemplateUpdateModel, TrainingTemplate>(model);
                trainingTemplate.CreatorId = userId;

                trainingTemplate = await traingingTemplateRepository.AddAsync(trainingTemplate);
            }
            else
            {
                trainingTemplate = await traingingTemplateRepository.GetByIdAsync(model.Id);

                if (trainingTemplate is not null)
                {
                    mapper.Map(model, trainingTemplate);
                    trainingTemplate = await traingingTemplateRepository.UpdateAsync(trainingTemplate);
                }
                else
                    return null;
            }

            await traingingTemplateRepository.SaveChangesAsync();

            return mapper.Map<TrainingTemplate, TrainingTemplateDto>(trainingTemplate);
        }

        public async Task<IEnumerable<TrainingTemplateDto>> GetTrainingTemplatesByUserIdAsync(
            SortedPaginationModel model,
            int userId)
        {
            User user = await userService.GetUserEntityByIdAsync(userId);
            
            if (user is null)
                return null;

            return paginationService.MakePage(model, user.TrainingTemplates);
        }

        public async Task<TrainingTemplateDto> AddTrainingTemplateAsync(TrainingTemplateModel model, int userId)
        {
            TrainingTemplateUpdateModel updateModel =
                mapper.Map<TrainingTemplateModel, TrainingTemplateUpdateModel>(model);

            return await UpsertTrainingTemplateAsync(updateModel, userId);
        }

        public async Task<TrainingTemplateDto> UpdateTrainingTemplateAsync(TrainingTemplateUpdateModel model, int userId)
        {
            return await UpsertTrainingTemplateAsync(model, userId);
        }

        public async Task<bool> DeleteTrainingTemplateAsync(int templateId, int userId)
        {
            TrainingTemplate trainingTemplate = await traingingTemplateRepository.GetByIdAsync(templateId);

            if (trainingTemplate is null || !trainingTemplate.CreatorId.Equals(userId))
                return false;

            await traingingTemplateRepository.DeleteAsync(trainingTemplate);
            await traingingTemplateRepository.SaveChangesAsync();
            
            return true;
        }

        public async Task<TrainingTemplateDto> GetTrainingTemplateByIdAsync(int templateId, int userId)
        {
            TrainingTemplate template = await traingingTemplateRepository.GetByIdAsync(templateId);

            if (template?.CreatorId != userId)
                return null;

            return mapper.Map<TrainingTemplate, TrainingTemplateDto>(template);
        }
        
        public async Task<TrainingTemplateDto> CloneForCreatorAsync(int templateId, int userId)
        {
            TrainingTemplate template = await traingingTemplateRepository.GetByIdAsync(templateId);

            if (template?.CreatorId != userId)
                return null;

            return await CloneAsync(template, userId);
        }

        public async Task<TrainingTemplateDto> CloneAsync(TrainingTemplate template, int userId)
        {
            User user = await userService.GetUserEntityByIdAsync(userId);

            if (template is null || user is null)
                return null;

            TrainingTemplate templateClone = template.Clone();
            templateClone.CreatorId = userId;

            TrainingTemplateDto templateDto = await AddTrainingTemplateAsync(
                mapper.Map<TrainingTemplate, TrainingTemplateModel>(templateClone), userId);

            if (templateDto is null)
                return null;

            templateClone = await traingingTemplateRepository.GetByIdAsync(templateDto.Id);

            List<Exercise> clonedExercises = template.Exercises.Select(e => e.Clone()).ToList();

            templateClone.Exercises = clonedExercises;

            templateClone = await traingingTemplateRepository.UpdateAsync(templateClone);
            await traingingTemplateRepository.SaveChangesAsync();

            return mapper.Map<TrainingTemplate, TrainingTemplateDto>(templateClone);
        }
    }
}
