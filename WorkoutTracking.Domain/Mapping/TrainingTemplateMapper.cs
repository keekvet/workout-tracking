using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.TrainingTemplate;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Mapping
{
    public class TrainingTemplateMapper : Profile
    {
        public TrainingTemplateMapper()
        {
            CreateMap<TrainingTemplate, TrainingTemplateDto>();
            CreateMap<TrainingTemplateModel, TrainingTemplate>();
            CreateMap<TrainingTemplateUpdateModel, TrainingTemplate>();
            CreateMap<TrainingTemplateUpdateModel, TrainingTemplateModel>().ReverseMap();
        }
    }
}
