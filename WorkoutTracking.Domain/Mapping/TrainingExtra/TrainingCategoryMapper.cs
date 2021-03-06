using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Mapping.TrainingExtra
{
    class TrainingCategoryMapper : Profile
    {
        public TrainingCategoryMapper()
        {
            CreateMap<TrainingCategory, TrainingCategoryDto>();
        }
    }
}
