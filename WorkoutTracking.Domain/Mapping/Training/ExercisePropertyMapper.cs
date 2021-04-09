using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.Training;
using WorkoutTracking.Application.Models.ExerciseProperty;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Mapping.Training
{
    public class ExercisePropertyMapper : Profile
    {
        public ExercisePropertyMapper()
        {
            CreateMap<ExercisePropertyModel, ExerciseProperty>();
            CreateMap<ExercisePropertyUpdateModel, ExerciseProperty>();
            CreateMap<ExerciseProperty, ExercisePropertyDto>();
        }
    }
}
