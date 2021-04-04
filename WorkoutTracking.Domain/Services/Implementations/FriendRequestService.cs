using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.FriendRequest;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Enums;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IFriendService friendService;
        private readonly IFollowingService followingService;
        private readonly IRepository<FriendRequest> friendRequestRepository;
        private readonly IPaginationService<FriendRequest, FriendRequestDto> paginationService;

        public FriendRequestService(
            IMapper mapper,
            IUserService userService,
            IFriendService friendService,
            IFollowingService followingService,
            IRepository<FriendRequest> friendRequestRepository,
            IPaginationService<FriendRequest, FriendRequestDto> paginationService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.friendService = friendService;
            this.followingService = followingService;
            this.paginationService = paginationService;
            this.friendRequestRepository = friendRequestRepository;
        }

        public async Task<IEnumerable<FriendRequestDto>> GetOutputFriendRequestsAsync(SortedPaginationModel model,
                                                                                 int userId)
        {
            Expression<Func<FriendRequest, bool>> filter = f => f.RequestFromId.Equals(userId);
            return await paginationService.GetRangeAsync(model, filter);
        }

        public async Task<IEnumerable<FriendRequestDto>> GetInputFriendRequestsAsync(SortedPaginationModel model,
                                                                                int userId)
        {
            Expression<Func<FriendRequest, bool>> filter = f => f.RequestToId.Equals(userId);
            return await paginationService.GetRangeAsync(model, filter);
        }

        public async Task<FriendRequestDto> AddFriendRequestAsync(int receiverId, int senderId) 
        {
            User userSender = await userService.GetUserEntityByIdAsync(senderId);
            User userReceiver = await userService.GetUserEntityByIdAsync(receiverId);

            if (
                userSender is null 
                || userReceiver is null
                || userSender.SendedFriendRequests.Where(f => f.RequestToId.Equals(receiverId)).Any()
                || userSender.SendedFriendRequests.Where(f => f.RequestToId.Equals(receiverId)).Any()
                || receiverId.Equals(senderId))
                return null;

            FriendRequest friendRequest = new FriendRequest()
            {
                State = FriendRequestState.Undefined,
                RequestFrom = userSender,
                RequestTo = userReceiver
            };

            FriendRequest requestResult = await friendRequestRepository.AddAsync(friendRequest);
            await friendRequestRepository.SaveChangesAsync();

            return mapper.Map<FriendRequest, FriendRequestDto>(requestResult);
        }

        public async Task<bool> RemoveFriendRequestAsync(int receiverId, int senderId)
        {
            FriendRequest friendRequest =
                await friendRequestRepository.GetByIdAsync(senderId, receiverId);

            if (friendRequest is null)
                return false;

            bool result = await friendRequestRepository.DeleteAsync(friendRequest);
            await friendRequestRepository.SaveChangesAsync();

            return result;
        }

        public async Task<FriendRequestDto> UpdateFriendRequestStateAsync(
            FriendRequestStateUpdateModel model, 
            int receiverId)
        {
            FriendRequest friendRequest =
                await friendRequestRepository.GetByIdAsync(model.SenderId, receiverId);

            if (friendRequest is null || !friendRequest.RequestToId.Equals(receiverId))
                return null;

            friendRequest.State = model.State;
            friendRequest = await friendRequestRepository.UpdateAsync(friendRequest);
            await friendRequestRepository.SaveChangesAsync();

            bool makeFriendResult = false;

            if (model.State is FriendRequestState.Accepted)
            {
                makeFriendResult =
                    await friendService.MakeFriendsAsync(friendRequest.RequestFromId, friendRequest.RequestToId);
            }

            if (!makeFriendResult)
                return null;

            await followingService.RemoveFollowingAsync(friendRequest.RequestToId, friendRequest.RequestFromId);
            await followingService.RemoveFollowingAsync(friendRequest.RequestFromId, friendRequest.RequestToId);

            return mapper.Map<FriendRequest, FriendRequestDto>(friendRequest);
        }


    }
}
