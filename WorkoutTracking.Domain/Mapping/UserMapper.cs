using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.User;

namespace WorkoutTracking.Application.Mapping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserLoginModel, User>();
            CreateMap<UserRegisterModel, User>();

            CreateMap<User, UserDto>();
            CreateMap<User, UserTokenDto>();
        }
    }
}
