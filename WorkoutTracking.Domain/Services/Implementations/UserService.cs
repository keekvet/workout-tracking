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
using WorkoutTracking.Domain.Dto;
using WorkoutTracking.Domain.Services.Interfaces;

namespace WorkoutTracking.Domain.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
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

        public async Task<ICollection<UserDto>> GetUsersRangeWithNameAsync(string text, int offset, int count)
        {
            return await userRepository.Entities
                .Where(u => u.Name.ToUpper().Contains(text.ToUpper()))
                .Skip(offset)
                .Take(count)
                .Select(u => mapper.Map<User, UserDto>(u))
                .ToListAsync();
        }

        public async Task<User> GetUserEntityByNameAsync(string name)
        {
            return await userRepository.Entities
                .Where(u => u.Name.Equals(name))
                .FirstOrDefaultAsync();
        }
    }
}
