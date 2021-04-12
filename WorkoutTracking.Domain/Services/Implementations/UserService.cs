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
using System.Linq.Expressions;
using WorkoutTracking.Application.Models.User;
using System.Security.Authentication;
using WorkoutTracking.Application.Dto.User;
using WorkoutTracking.Data.Enums;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IPaginationService<User, UserDto> paginationService;
        private readonly IEncryptionService encryptionService;
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;

        public UserService(
            IPaginationService<User, UserDto> paginationService,
            IEncryptionService encryptionService,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            this.paginationService = paginationService;
            this.userRepository = userRepository;
            this.encryptionService = encryptionService;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            bool result = await userRepository.DeleteAsync(await userRepository.GetByIdAsync(userId));
            await userRepository.SaveChangesAsync();
            return result;
        }

        public async Task<UserDto> GetUserByIdAsync(int id, int userId)
        {
            if ((await userRepository.GetByIdAsync(userId)) is null)
                return null;

            return mapper.Map<User, UserDto>(await userRepository.GetByIdAsync(id));
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return mapper.Map<User, UserDto>(await userRepository.GetByIdAsync(id));
        }
        public async Task<UserDto> GetUserByNameAsync(string name, int userId)
        {
            if ((await userRepository.GetByIdAsync(userId)) is null)
                return null;

            return mapper.Map<User, UserDto>(await GetUserEntityByNameAsync(name));
        }

        public async Task<UserDto> GetUserByNameAsync(string name)
        {
            return mapper.Map<User, UserDto>(await GetUserEntityByNameAsync(name));
        }

        public async Task<IEnumerable<UserDto>> GetUsersWithNameAsync(UserSearchPaginationModel model)
        {
            Expression<Func<User, bool>> filter = null;

            if (model.SearchWithName is not null)
                filter = u => u.Name.ToUpper().Contains(model.SearchWithName.ToUpper());

            return await paginationService.GetRangeAsync(model, filter);
        }

        public async Task<User> GetUserEntityByNameAsync(string name)
        {
            return await userRepository.GetAll()
                .Where(u => u.Name.Equals(name))
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserEntityByIdAsync(int Id)
        {
            return await userRepository.GetByIdAsync(Id);
        }

        public async Task<UserDto> UpdateUserAsync(UserUpdateModel userUpdateModel, int userId)
        {
            User user = await userRepository.GetByIdAsync(userId);
            
            UserAccess parseResult;

            if (user is null || !Enum.TryParse(userUpdateModel.Access, true, out parseResult))
                return null;

            if (!user.Name.Equals(userUpdateModel.Name))
                if (await GetUserByNameAsync(userUpdateModel.Name) is not null)
                    return null;

            user.Name = userUpdateModel.Name;
            user.About = userUpdateModel.About;
            user.Access = parseResult;

            UserDto dto = mapper.Map<User, UserDto>(await userRepository.UpdateAsync(user));
            await userRepository.SaveChangesAsync();
            return dto;

        }
        public async Task<bool> UpdatePasswordAsync(PasswordUpdateModel updatePasswordModel, int userId)
        {
            User user = await userRepository.GetByIdAsync(userId);

            if (user is null 
                || !await encryptionService.PasswordEqualsHashAsync(
                    updatePasswordModel.CurrentPassword, user.PasswordHash, user.Salt))
                return false;

            HashedPassword newHashedPassword = await encryptionService.EncryptAsync(
                Encoding.UTF8.GetBytes(updatePasswordModel.NewPassword));

            user.PasswordHash = newHashedPassword.Hash;
            user.Salt = newHashedPassword.Salt;

            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();

            return true;
        }
    }
}
