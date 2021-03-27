using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Domain.Dto;
using WorkoutTracking.Domain.Models.User;

namespace WorkoutTracking.Domain.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<UserDto> Register(UserRegisterModel user);
    }
}
