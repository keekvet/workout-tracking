using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IFriendService
    {
        Task<bool> MakeFriendsAsync(int requestFromId, int requestToId);
        Task<IEnumerable<UserDto>> GetFriendsAsync(FriendPaginationModel model);
        Task<IEnumerable<UserDto>> GetFriendsById(int userId);
    }
}
