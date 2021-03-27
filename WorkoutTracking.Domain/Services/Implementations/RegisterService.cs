using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;
using WorkoutTracking.Domain.Dto;
using WorkoutTracking.Domain.Models;
using WorkoutTracking.Domain.Models.User;
using WorkoutTracking.Domain.Services.Interfaces;

namespace WorkoutTracking.Domain.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;
        private readonly IUserService userService;
        private readonly IRepository<User> userRepository;
        private readonly IEncryptionService encryptionService;

        public RegisterService(
            IMapper mapper,
            IJwtService jwtService,
            IUserService userService, 
            IRepository<User> userRepository,
            IEncryptionService encryptionService)
        {
            this.mapper = mapper;
            this.jwtService = jwtService;
            this.userService = userService;
            this.userRepository = userRepository;
            this.encryptionService = encryptionService;
        }

        public async Task<UserDto> Register(UserRegisterModel userModel)
        {
            UserDto foundUser = await userService.GetUserByNameAsync(userModel.Name);

            if (foundUser is not null)
                return null;

            HashedPassword passwordHash = await encryptionService.EncryptAsync(
               Encoding.UTF8.GetBytes(userModel.Password));

            User newUser = mapper.Map<UserRegisterModel, User>(userModel);
            newUser.PasswordHash = passwordHash.Hash;
            newUser.Salt = passwordHash.Salt;

            newUser.Jwt = jwtService.GenerateToken(newUser);

            User addedUser = await userRepository.AddAsync(newUser);
            await userRepository.SaveChangesAsync();

            return mapper.Map<User, UserDto>(addedUser);
        }
    }
}
