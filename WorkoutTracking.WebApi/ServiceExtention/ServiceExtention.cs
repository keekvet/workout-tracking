using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;
using WorkoutTracking.Application.ConfigurationTemplates;
using WorkoutTracking.Application.Services.Implementations;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Application.Dto;

namespace Workout_tracking.ServiceExtention
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region services
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserResolverService, UserResolverService>();

            services.AddScoped<IFollowingService, FollowingService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<IFriendRequestService, FriendRequestService>();

            services.AddScoped<ITrainingCategoryService, TrainingCategoryService>();
            services.AddScoped<IPublicTrainingTemplateService, PublicTrainingTemplateService>();
            services.AddScoped<IScheduledTrainingService, ScheduledTrainingService>();

            services.AddScoped<ITrainingTemplateService, TrainingTemplateService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IExercisePropertyService, ExercisePropertyService>();

            services.AddScoped<ITrainingHistoryService, TrainingHistoryService>();
            services.AddScoped<IExerciseHistoryService, ExerciseHistoryService>();
            services.AddScoped<IExercisePropertyHistoryService, ExercisePropertyHistoryService>();
            #endregion

            #region pagination services
            services.AddScoped<
                IPaginationService<User, UserDto>,
                PaginationService<User, UserDto>>();
            services.AddScoped<
                IPaginationService<FriendRequest, FriendRequestDto>,
                PaginationService<FriendRequest, FriendRequestDto>>();
            services.AddScoped<
                IPaginationService<TrainingCategory, TrainingCategoryDto>,
                PaginationService<TrainingCategory, TrainingCategoryDto>>();
            services.AddScoped<
                IPaginationService<TrainingTemplate, TrainingTemplateDto>,
                PaginationService<TrainingTemplate, TrainingTemplateDto>>();
            services.AddScoped<
                IPaginationService<PublicTrainingTemplate, PublicTrainingTemplateDto>,
                PaginationService<PublicTrainingTemplate, PublicTrainingTemplateDto>>();
            services.AddScoped<
                IPaginationService<ScheduledTraining, ScheduledTrainingDto>,
                PaginationService<ScheduledTraining, ScheduledTrainingDto>>();
            services.AddScoped<
                IPaginationService<TrainingHistory, TrainingHistoryDto>,
                PaginationService<TrainingHistory, TrainingHistoryDto>>();
            #endregion

            #region repositories
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<FriendRequest>, Repository<FriendRequest>>();
           
            services.AddScoped<IRepository<TrainingCategory>, Repository<TrainingCategory>>();
            services.AddScoped<IRepository<PublicTrainingTemplate>, Repository<PublicTrainingTemplate>>();
            services.AddScoped<IRepository<ScheduledTraining>, Repository<ScheduledTraining>>();
            
            services.AddScoped<IRepository<TrainingTemplate>, Repository<TrainingTemplate>>();
            services.AddScoped<IRepository<Exercise>, Repository<Exercise>>();
            services.AddScoped<IRepository<ExerciseProperty>, Repository<ExerciseProperty>>();

            services.AddScoped<IRepository<TrainingHistory>, Repository<TrainingHistory>>();
            services.AddScoped<IRepository<ExerciseHistory>, Repository<ExerciseHistory>>();
            services.AddScoped<IRepository<ExercisePropertyHistory>, Repository<ExercisePropertyHistory>>();
            #endregion

            return services;
        }

        public static IServiceCollection AddConfiguration(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));
            services.Configure<EncryptionConfiguration>(configuration.GetSection(nameof(EncryptionConfiguration)));
            return services;
        }
    }
}
