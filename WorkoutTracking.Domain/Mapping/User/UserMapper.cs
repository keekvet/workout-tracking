using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = WorkoutTracking.Data.Entities.User;
using WorkoutTracking.Application.Models.User;
using WorkoutTracking.Application.Dto.User;

namespace WorkoutTracking.Application.Mapping.User
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserLoginModel, UserEntity>();
            CreateMap<UserRegisterModel, UserEntity>();

            CreateMap<UserEntity, UserDto>();
            CreateMap<UserEntity, UserTokenDto>();
        }
    }
}
