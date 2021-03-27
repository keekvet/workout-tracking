using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Domain.Dto;
using WorkoutTracking.Domain.Models;
using WorkoutTracking.Domain.Models.User;

namespace WorkoutTracking.Domain.Mapping
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
