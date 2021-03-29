using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.Pagination;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IPaginationService<User, UserDto> paginationService;
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;

        public UserService(
            IPaginationService<User, UserDto> paginationService,
            IRepository<User> userRepository, 
            IMapper mapper)
        {
            this.paginationService = paginationService;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            bool result = await userRepository.DeleteAsync(user);
            await userRepository.SaveChangesAsync();
            return result;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return mapper.Map<User, UserDto>(await userRepository.GetByIdAsync(id));
        }

        public async Task<UserDto> GetUserByNameAsync(string name)
        {
            return mapper.Map<User, UserDto>(await GetUserEntityByNameAsync(name));
        }

        public async Task<ICollection<UserDto>> GetUsersWithNameAsync(UserSearchModel model)
        {
            Func<User, bool> filter = null;

            if (model.Name is not null)
                filter = u => u.Name.Contains(model.Name);

            return await paginationService.GetRangeAsync(model, filter);
        }

        public async Task<User> GetUserEntityByNameAsync(string name)
        {
            return await userRepository.GetAll()
                .Where(u => u.Name.Equals(name))
                .FirstOrDefaultAsync();
        }
    }
}
