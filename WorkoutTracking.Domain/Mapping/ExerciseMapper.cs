using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.Exercise;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Mapping
{
    public class ExerciseMapper : Profile
    {
        public ExerciseMapper()
        {
            CreateMap<ExerciseModel, Exercise>();
            CreateMap<ExerciseUpdateModel, Exercise>();
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<Exercise, Exercise>();
        }
    }
}
