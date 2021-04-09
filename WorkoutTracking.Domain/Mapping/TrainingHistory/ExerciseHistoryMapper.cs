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
    public class ExerciseHistoryMapper : Profile
    {
        public ExerciseHistoryMapper()
        {
            CreateMap<Exercise, ExerciseHistory>()
                .ForMember(e => e.Id, opt => opt.Ignore());

            CreateMap<ExerciseHistory, ExerciseHistoryDto>();
        }
    }
}
