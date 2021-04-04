using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.TrainingTemplate;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class TrainingTemplateService : ITrainingTemplateService
    {
        IMapper mapper;
        IUserService userService;
        ITrainingCategoryService trainingCategoryService;
        IRepository<TrainingTemplate> traingingTemplateRepository;
        IPaginationService<TrainingTemplate, TrainingTemplateDto> paginationService;

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
            if (await trainingCategoryService.GetCategoryById(model.CategoryId) is null)
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

        public async Task<IEnumerable<TrainingTemplateDto>> GetTrainingTemplatesByUserIdAsync(int userId)
        {
            return (await userService.GetUserEntityByIdAsync(userId))?.TrainingTemplates
                .Select(t => mapper.Map<TrainingTemplate, TrainingTemplateDto>(t));
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
    }
}
