using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto.TrainingHistory;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Mapping.TrainingHistory
{
    public class ExercisePropertyHistoryMapper : Profile
    {
        public ExercisePropertyHistoryMapper()
        {
            CreateMap<ExerciseProperty, ExercisePropertyHistory>()
                .ForMember(h => h.Id, opt => opt.Ignore());

            CreateMap<ExercisePropertyHistory, ExercisePropertyHistoryDto>();
        }
    }
}
