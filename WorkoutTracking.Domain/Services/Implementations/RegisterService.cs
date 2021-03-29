using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Repositories;
using WorkoutTracking.Application.Dto;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.User;
using WorkoutTracking.Application.Services.Interfaces;
using System.Security.Authentication;
using System.Text.RegularExpressions;

namespace WorkoutTracking.Application.Services.Implementations
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
                throw new InvalidCredentialException("User with this name already exist");

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
