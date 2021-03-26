using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;
using WorkoutTracking.Domain.Services.Interfaces;

namespace WorkoutTracking.Domain.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await userRepository.AddAsync(user);
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            return await userRepository.DeleteAsync(user);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            if (name is null)
                return null;
            return await Task.Run(() => userRepository.Entities.Where(u => u.Name.Equals(name)).FirstOrDefault());
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await userRepository.UpdateAsync(user);
        }
    }
}
