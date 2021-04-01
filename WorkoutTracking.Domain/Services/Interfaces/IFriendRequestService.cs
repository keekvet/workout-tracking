using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.Pagination.Base;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IFriendRequestService
    {
        Task<FriendRequestDto> AddFriendRequestAsync(int receiverId, int sernderId);
        Task<IEnumerable<FriendRequestDto>> GetInputFriendRequestsAsync(SortedPaginationModel model, int userId);
        Task<IEnumerable<FriendRequestDto>> GetOutputFriendRequestsAsync(SortedPaginationModel model, int userId);
        Task<FriendRequestDto> UpdateFriendRequestStateAsync(FriendRequestStateUpdateModel model, int receiverId);
        Task<bool> RemoveFriendRequestAsync(int receiverId, int senderId);
    }
}
