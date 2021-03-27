using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Domain.Dto;

namespace WorkoutTracking.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> DeleteUserAsync(User user);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> GetUserByNameAsync(string name);
        Task<User> GetUserEntityByNameAsync(string name);
        Task<ICollection<UserDto>> GetUsersRangeWithNameAsync(string text, int offset, int count); 
    }
}
