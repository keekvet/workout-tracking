using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Models.TrainingTemplate;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Mapping.Training
{
    public class TrainingTemplateMapper : Profile
    {
        public TrainingTemplateMapper()
        {
            CreateMap<TrainingTemplate, TrainingTemplateDto>();
            CreateMap<TrainingTemplateModel, TrainingTemplate>().ReverseMap();
            CreateMap<TrainingTemplateUpdateModel, TrainingTemplate>();
            CreateMap<TrainingTemplateUpdateModel, TrainingTemplateModel>().ReverseMap();
        }
    }
}
