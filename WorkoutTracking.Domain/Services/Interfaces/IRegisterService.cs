using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.User;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<UserDto> Register(UserRegisterModel user);
    }
}
