using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.TrainingExtra;
using WorkoutTracking.Application.Models.ScheduledTraining;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Mapping.TrainingExtra
{
    public class ScheduledTrainingMapper : Profile
    {
        public ScheduledTrainingMapper()
        {
            CreateMap<ScheduledTrainingModel, ScheduledTraining>()
                .ForMember(s => s.StartTime, opt => opt.MapFrom(m => TimeSpan.Parse(m.StartTime)));

            CreateMap<ScheduledTraining, ScheduledTrainingDto>()
                .ForMember(d => d.StartTime, opt => opt.MapFrom(m => m.StartTime.ToString()));

        }
    }
}
