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
        IPaginationService<User, UserDto> paginationService;
        public FriendService(
            IMapper mapper,
            IUserService userService,
            IRepository<User> userRepository,
            IPaginationService<User, UserDto> paginationService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.userRepository = userRepository;
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

           
            return true;
        }

        public async Task<IEnumerable<UserDto>> GetFriendsAsync(FriendPaginationModel model)
        {
            User user = await userService.GetUserEntityByIdAsync(model.UserId);

            IEnumerable<User> friends = user?.FriendsFrom.Union(user.FriendsTo);

            if (friends is null)
                return null;

            return paginationService.MakePage(model, friends);
        }

        public async Task<IEnumerable<UserDto>> GetFriendsByIdAsync(int userId)
        {
            User user = await userService.GetUserEntityByIdAsync(userId);

            return user?.FriendsFrom.Union(user.FriendsTo).Select(u => mapper.Map<User, UserDto>(u));
        }

        public async Task<bool> RemoveFriendAsync(int friendId, int userId)
        {
            IEnumerable<UserDto> friends = await GetFriendsByIdAsync(userId);

            if (friends is null || !friends.Where(u => u.Id.Equals(friendId)).Any())
                return false;

            User user = await userService.GetUserEntityByIdAsync(userId);
            User friend = await userService.GetUserEntityByIdAsync(friendId);

            if (!user.FriendsFrom.Remove(friend))
                user.FriendsTo.Remove(friend);

            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();

            return true;
        }
    }
}
