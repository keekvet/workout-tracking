using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Mapping
{
    public class PublicTrainingTemplateMapper : Profile
    {
        public PublicTrainingTemplateMapper()
        {
            CreateMap<PublicTrainingTemplate, PublicTrainingTemplateDto>()
                .ForMember(p => p.PublicId, opt => opt.MapFrom(p => p.Id))
                .ForMember(p => p.TemplateId, opt => opt.MapFrom(p => p.Template.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(p => p.Template.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(p => p.Template.Description))
                .ForMember(p => p.CreatorId, opt => opt.MapFrom(p => p.Template.CreatorId))
                .ForMember(p => p.CategoryId, opt => opt.MapFrom(p => p.Template.CategoryId));
        }
    }
}
