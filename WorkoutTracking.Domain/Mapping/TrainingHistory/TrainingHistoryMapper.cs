using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Dto.TrainingHistory;
using WorkoutTracking.Data.Entities;
using TrainingHistoryEntity = WorkoutTracking.Data.Entities.TrainingHistory ;

namespace WorkoutTracking.Application.Mapping.TrainingHistory
{
    public class TrainingHistoryMapper : Profile
    {
        public TrainingHistoryMapper()
        {
            CreateMap<TrainingTemplate, TrainingHistoryEntity>()
                .ForMember(h => h.UserId, opt => opt.MapFrom(t => t.CreatorId))
                .ForMember(h => h.Id, opt => opt.Ignore());

            CreateMap<TrainingTemplateDto, TrainingHistoryEntity>()
                .ForMember(h => h.UserId, opt => opt.MapFrom(t => t.CreatorId))
                .ForMember(h => h.Id, opt => opt.Ignore());
            
            CreateMap<TrainingHistoryEntity, TrainingHistoryDto>();
        }
    }
}
