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
using WorkoutTracking.Application.Dto.User;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;
        private readonly IUserService userService;
        private readonly IEncryptionService encryptionService;

        public LoginService(
            IMapper mapper,
            IJwtService jwtService,
            IUserService userService,
            IEncryptionService encryptionService)
        {
            this.mapper = mapper;
            this.jwtService = jwtService;
            this.userService = userService;
            this.encryptionService = encryptionService;
        }



        public async Task<UserTokenDto> LoginAsync(UserLoginModel user)
        {
            User foundUser = await userService.GetUserEntityByNameAsync(user.Name);

            if (foundUser is null ||
                !await encryptionService.PasswordEqualsHashAsync(user.Password, foundUser.PasswordHash, foundUser.Salt))
            {
                throw new InvalidCredentialException("user name or password incorrect");
            }

            foundUser.Jwt = jwtService.GenerateToken(foundUser);

            return mapper.Map<User, UserTokenDto>(foundUser);
        }

    }
}
