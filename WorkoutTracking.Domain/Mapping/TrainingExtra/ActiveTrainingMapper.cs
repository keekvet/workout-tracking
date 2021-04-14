using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Mapping.TrainingExtra
{
    public class ActiveTrainingMapper : Profile
    {
        public ActiveTrainingMapper()
        {
            CreateMap<ActiveTraining, ActiveTrainingDto>()
                .ForMember(d => d.TrainingTemplateName, opt => opt.MapFrom(a => a.TrainingTemplate.Name));
        }
    }
}
