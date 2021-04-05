using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{

    public class PublicTrainingTemplateService : IPublicTrainingTemplateService
    {
        private readonly IMapper mapper;
        private readonly ITrainingTemplateService templateService;
        private readonly IRepository<TrainingTemplate> templateRepository;
        private readonly IRepository<PublicTrainingTemplate> publicTemplateRepository;
        private readonly IPaginationService<PublicTrainingTemplate, PublicTrainingTemplateDto> paginationService;

        public PublicTrainingTemplateService(
            IMapper mapper,
            ITrainingTemplateService templateService,
            IRepository<TrainingTemplate> templateRepository,
            IRepository<PublicTrainingTemplate> publicTemplateRepository,
            IPaginationService<PublicTrainingTemplate, PublicTrainingTemplateDto> paginationService)
        {
            this.mapper = mapper;
            this.templateService = templateService;
            this.paginationService = paginationService;
            this.templateRepository = templateRepository;
            this.publicTemplateRepository = publicTemplateRepository;
        }

        public async Task<IEnumerable<PublicTrainingTemplateDto>> GetPublicTemplatesAsync(
            PublicTrainingTemplatePaginationModel model, int? userId)
        {
            IQueryable<PublicTrainingTemplate> publicTemplates = publicTemplateRepository.GetAll();

            if (model.Name is not null)
                publicTemplates = publicTemplates.Where(p => p.Template.Name.Contains(model.Name));

            if (model.CategoryId is not null)
                publicTemplates = publicTemplates.Where(p => p.Template.CategoryId.Equals(model.CategoryId));
            
            if (userId is not null)
                publicTemplates = publicTemplates.Where(p => p.Template.CreatorId.Equals(userId));

            IEnumerable<PublicTrainingTemplateDto> publicTemplatesDtos = (await publicTemplates
                .ToListAsync())
                .Select(p => mapper.Map<PublicTrainingTemplate, PublicTrainingTemplateDto>(p));
                

            return paginationService.MakePage(model, publicTemplatesDtos);
        }

        public async Task<PublicTrainingTemplateDto> AddPublicTemplateAsync(int templateId, int userId)
        {
            TrainingTemplate template = await templateRepository.GetByIdAsync(templateId);

            if (template is null 
                || template.CreatorId != userId
                || template.publicTraining is not null)
                return null;

            PublicTrainingTemplate publicTemplate = 
                await publicTemplateRepository.AddAsync(new PublicTrainingTemplate() { Template = template });
            await publicTemplateRepository.SaveChangesAsync();

            return mapper.Map<PublicTrainingTemplate, PublicTrainingTemplateDto>(publicTemplate);
        }

        public async Task<bool> DeletePublicTemplateAsync(int templateId, int userId)
        {
            PublicTrainingTemplate template = await publicTemplateRepository.GetByIdAsync(templateId);

            if (template is null || template.Template.CreatorId != userId)
                return false;

            await publicTemplateRepository.DeleteAsync(template);
            await publicTemplateRepository.SaveChangesAsync();

            return true;
        }

        public async Task<TrainingTemplateDto> ClonePublicTemplateAsync(int publicTemplateId, int userId)
        {
            TrainingTemplate trainingTemplate =
                (await publicTemplateRepository.GetByIdAsync(publicTemplateId))?.Template;

            return await templateService.CloneAsync(trainingTemplate, userId);
        }
    }
}
