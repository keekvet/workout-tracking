using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Dto.User;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IFollowingService
    {
        Task<bool> AddFollowingAsync(int userToFollow, int userId);
        Task<bool> RemoveFollowingAsync(int userToUnfollow, int userId);
        Task<IEnumerable<UserDto>> GetFollowingAsync(FollowingPaginationModel model);
        Task<IEnumerable<UserDto>> GetFollowersAsync(FollowingPaginationModel model);
    }
}
