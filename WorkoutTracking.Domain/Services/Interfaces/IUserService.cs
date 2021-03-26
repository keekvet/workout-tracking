using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByNameAsync(string name);
    }
}
