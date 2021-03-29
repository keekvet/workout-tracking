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
            if (userModel is null)
                return null;

            #region credentials validation
            if (!userModel.Password.Equals(userModel.PasswordConfirmation))
                throw new InvalidCredentialException("mistake in password");

            if (userModel.Password.Equals(userModel.Name))
                throw new InvalidCredentialException("user name and password are equal");

            if (!Regex.IsMatch(userModel.Name, @"^\w{4,20}$"))
                throw new InvalidCredentialException("user name is too short or contains wrong symbols");

            string digit = @"\d+";
            string lowerCase = @"[a-z]+";
            string upperCase = @"[A-Z]+";
            string whiteSpaces = @"^\w{8, 20}$";

            if (!(
                Regex.IsMatch(userModel.Password, digit)
                && Regex.IsMatch(userModel.Password, lowerCase)
                && Regex.IsMatch(userModel.Password, upperCase)
                && Regex.IsMatch(userModel.Password, whiteSpaces)))
                throw new InvalidCredentialException("password must be at least 8 characters, " +
                    "no more than 20 characters, and must include at least one upper case letter, " +
                    "one lower case letter, and one numeric digit.");

            UserDto foundUser = await userService.GetUserByNameAsync(userModel.Name);

            if (foundUser is not null)
                throw new InvalidCredentialException("User with this name already exist");
            #endregion

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
