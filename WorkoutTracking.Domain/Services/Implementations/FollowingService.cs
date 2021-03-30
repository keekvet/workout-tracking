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
        IUserService userService;
        IRepository<User> userRepository;
        IPaginationService<User, UserDto> paginationService;

        public FollowingService(
            IPaginationService<User, UserDto> paginationService,
            IRepository<User> userRepository,
            IUserService userService, 
            IMapper mapper)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.userRepository = userRepository;
            this.paginationService = paginationService;
        }

        public async Task<ICollection<UserDto>> AddFollowingAsync(FollowingPaginationModel model, int userId)
        {
            User user = await userService.GetUserEntityByIdAsync(userId);
            User userToFollow = await userService.GetUserEntityByNameAsync(model.UserName);

            if (user is null || userToFollow is null || user.Following.Contains(userToFollow))
                return null;

            user.Following.Add(userToFollow);
            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();

            Expression<Func<User, bool>> filter = u => user.Following.Contains(u);

            return await paginationService.GetRangeAsync(model, filter);
        }

        public async Task<ICollection<UserDto>> RemoveFollowingAsync(FollowingPaginationModel model, int userId)
        {
            User user = await userService.GetUserEntityByIdAsync(userId);

            if (user is null)
                return null;

            User userToRemove = user.Following.Where(u => u.Name.Equals(model.UserName)).FirstOrDefault();
            user.Following.Remove(userToRemove);

            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();

            Expression<Func<User, bool>> filter = u => user.Following.Contains(u);

            return await paginationService.GetRangeAsync(model, filter);
        }

        public async Task<ICollection<UserDto>> GetFollowingAsync(FollowingPaginationModel model)
        {
            User user = await userService.GetUserEntityByNameAsync(model.UserName);

            if (user is null)
                return null;

            Expression<Func<User, bool>> filter = u => user.Following.Contains(u);

            return await paginationService.GetRangeAsync(model, filter);
        }

        public async Task<ICollection<UserDto>> GetFollowersAsync(FollowingPaginationModel model)
        {
            User user = await userService.GetUserEntityByNameAsync(model.UserName);

            if (user is null)
                return null;

            Expression<Func<User, bool>> filter = u => user.Followers.Contains(u);

            return await paginationService.GetRangeAsync(model, filter);
        }
    }
}
