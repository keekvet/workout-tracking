﻿using Microsoft.AspNetCore.Builder;
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserResolverService, UserResolverService>();
            services.AddScoped<IFollowingService, FollowingService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<IFriendRequestService, FriendRequestService>();
            #endregion
            
            #region pagiantion services
            services.AddScoped<
                IPaginationService<User, UserDto>, 
                PaginationService<User, UserDto>>();
            services.AddScoped<
                IPaginationService<FriendRequest, FriendRequestDto>, 
                PaginationService<FriendRequest, FriendRequestDto>>();
            #endregion

            #region repositories
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<FriendRequest>, Repository<FriendRequest>>();
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
