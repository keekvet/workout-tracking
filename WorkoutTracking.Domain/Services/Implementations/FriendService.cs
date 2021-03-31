using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Repositories;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models.Pagination;
using System.Linq.Expressions;
using AutoMapper;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class FriendService : IFriendService
    {
        IMapper mapper;
        IUserService userService;
        IRepository<User> userRepository;
        IFollowingService followingService;
        IPaginationService<User, UserDto> paginationService;
        public FriendService(
            IMapper mapper,
            IUserService userService,
            IRepository<User> userRepository,
            IFollowingService followingService,
            IPaginationService<User, UserDto> paginationService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.userRepository = userRepository;
            this.followingService = followingService;
            this.paginationService = paginationService;
        }

        public async Task<bool> MakeFriendsAsync(int requestFromId, int requestToId)
        {
            User userRequestTo = await userService.GetUserEntityByIdAsync(requestToId);
            User userRequestFrom = await userService.GetUserEntityByIdAsync(requestFromId);


            if (userRequestFrom is null || userRequestTo is null)
                return false;

            userRequestFrom.FriendsFrom.Add(userRequestTo);
            await userRepository.UpdateAsync(userRequestFrom);
            await userRepository.SaveChangesAsync();

            await followingService.RemoveFollowingAsync(requestToId, requestFromId);
            await followingService.RemoveFollowingAsync(requestFromId, requestToId);

            return true;
        }

        public async Task<IEnumerable<UserDto>> GetFriendsAsync(FriendPaginationModel model)
        {
            User user = await userService.GetUserEntityByIdAsync(model.UserId);

            IEnumerable<User> friends = user.FriendsFrom.Union(user.FriendsTo);
            return paginationService.MakePage(model, friends);
        }

        public async Task<IEnumerable<UserDto>> GetFriendsById(int userId)
        {
            User user = await userService.GetUserEntityByIdAsync(userId);

            return user.FriendsFrom.Union(user.FriendsTo).Select(u => mapper.Map<User, UserDto>(u));
        }
    }
}
