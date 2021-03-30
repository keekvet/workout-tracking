using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Models.User;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> DeleteUserAsync(int userId);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> GetUserByNameAsync(string name);
        Task<User> GetUserEntityByIdAsync(int id);
        Task<User> GetUserEntityByNameAsync(string name);
        Task<ICollection<UserDto>> GetUsersWithNameAsync(UserSearchModel model);
        Task<UserDto> UpdateUserAsync(UserUpdateModel userUpdateModel, int userId);
        Task<bool?> UpdatePasswordAsync(PasswordUpdateModel updatePasswordModel, int userId);
    }
}
