using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.User;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Mapping.User
{
    public class FriendRequestMapper : Profile
    {
        public FriendRequestMapper()
        {
            CreateMap<FriendRequest, FriendRequestDto>();
        }
    }
}
