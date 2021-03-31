using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class FollowingService : IFollowingService
    {
        IMapper mapper;
        IFriendService friendService;
        private readonly IUserService userService;
        private readonly IRepository<User> userRepository;
        private readonly IPaginationService<User, UserDto> paginationService;

        public FollowingService(
            IPaginationService<User, UserDto> paginationService,
            IRepository<User> userRepository,
            IFriendService friendService,
            IUserService userService, 
            IMapper mapper)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.friendService = friendService;
            this.userRepository = userRepository;
            this.paginationService = paginationService;
        }

        public async Task<bool> AddFollowingAsync(int userToFollowId, int userId)
        {
            User user = await userService.GetUserEntityByIdAsync(userId);
            User userToFollow = await userService.GetUserEntityByIdAsync(userToFollowId);

            if (user is null 
                || userToFollow is null 
                || user.Following.Contains(userToFollow) 
                || (await friendService.GetFriendsById(userId)).Where(u => u.Id.Equals(userToFollowId)).Any())
                return false;

            user.Following.Add(userToFollow);
            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFollowingAsync(int userToUnfollow, int userId)
        {
            User user = await userService.GetUserEntityByIdAsync(userId);

            if (user is null)
                return false;

            User userToRemove = user.Following.Where(u => u.Id.Equals(userToUnfollow)).FirstOrDefault();
            user.Following.Remove(userToRemove);

            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserDto>> GetFollowingAsync(FollowingPaginationModel model)
        {
            User user = await userService.GetUserEntityByIdAsync(model.UserId);

            if (user is null)
                return null;

            Expression<Func<User, bool>> filter = u => user.Following.Contains(u);

            return await paginationService.GetRangeAsync(model, filter);
        }

        public async Task<IEnumerable<UserDto>> GetFollowersAsync(FollowingPaginationModel model)
        {
            User user = await userService.GetUserEntityByIdAsync(model.UserId);

            if (user is null)
                return null;

            Expression<Func<User, bool>> filter = u => user.Followers.Contains(u);

            return await paginationService.GetRangeAsync(model, filter);
        }
    }
}
