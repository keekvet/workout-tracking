using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

    public class PublicTrainingTemplateService : IPublicTrainingTemplateService
    {
        private readonly IMapper mapper;
        private readonly IRepository<TrainingTemplate> templateRepository;
        private readonly IRepository<PublicTrainingTemplate> publicTemplateRepository;
        private readonly IPaginationService<PublicTrainingTemplate, PublicTrainingTemplateDto> paginationService;

        public PublicTrainingTemplateService(
            IMapper mapper,
            IRepository<TrainingTemplate> templateRepository,
            IRepository<PublicTrainingTemplate> publicTemplateRepository,
            IPaginationService<PublicTrainingTemplate, PublicTrainingTemplateDto> paginationService)
        {
            this.mapper = mapper;
            this.paginationService = paginationService;
            this.templateRepository = templateRepository;
            this.publicTemplateRepository = publicTemplateRepository;
        }

        public async Task<IEnumerable<PublicTrainingTemplateDto>> GetPublicTemplatesAsync(SortedPaginationModel model)
        {
            IEnumerable<PublicTrainingTemplateDto> publicTemplates =
                await publicTemplateRepository
                .GetAll()
                .Select(p => mapper.Map<PublicTrainingTemplate, PublicTrainingTemplateDto>(p))
                .ToListAsync();

            return paginationService.MakePage(model, publicTemplates);
        }

        public async Task<PublicTrainingTemplateDto> AddPublicTemplateAsync(int templateId, int userId)
        {
            TrainingTemplate template = await templateRepository.GetByIdAsync(templateId);

            if (template is not null || !template.CreatorId.Equals(userId))
                return null;

            PublicTrainingTemplate publicTemplate = 
                await publicTemplateRepository.AddAsync(new PublicTrainingTemplate() { Template = template });
            await publicTemplateRepository.SaveChangesAsync();

            return mapper.Map<PublicTrainingTemplate, PublicTrainingTemplateDto>(publicTemplate);
        }

        public async Task<bool> DeletePublicTemplateAsync(int templateId, int userId)
        {
            PublicTrainingTemplate template = await publicTemplateRepository.GetByIdAsync(templateId);

            if (template is null)
                return false;

            await publicTemplateRepository.DeleteAsync(template);
            await publicTemplateRepository.SaveChangesAsync();

            return true;
        }
    }
}
