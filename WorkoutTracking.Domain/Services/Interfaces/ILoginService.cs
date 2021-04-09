using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.User;
using WorkoutTracking.Application.Dto.User;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface ILoginService
    {
        Task<UserTokenDto> LoginAsync(UserLoginModel user);
    }
}
