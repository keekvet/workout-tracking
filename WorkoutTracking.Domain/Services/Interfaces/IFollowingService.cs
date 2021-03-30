using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IFollowingService
    {
        Task<ICollection<UserDto>> AddFollowingAsync(FollowingPaginationModel model, int userId);
        Task<ICollection<UserDto>> RemoveFollowingAsync(FollowingPaginationModel model, int userId);
        Task<ICollection<UserDto>> GetFollowingAsync(FollowingPaginationModel model);
        Task<ICollection<UserDto>> GetFollowersAsync(FollowingPaginationModel model);
    }
}
