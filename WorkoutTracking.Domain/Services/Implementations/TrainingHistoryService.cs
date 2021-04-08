using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class TrainingHistoryService : ITrainingHistoryService
    {
        private readonly IMapper mapper;
        private readonly IRepository<TrainingHistory> trainingHistoryRepository;

        public TrainingHistoryService(
            IMapper mapper, 
            IRepository<TrainingHistory> trainingHistoryRepository)
        {
            this.mapper = mapper;
            this.trainingHistoryRepository = trainingHistoryRepository;
        }


    }
}
