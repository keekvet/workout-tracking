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

            if (foundUser is not null)
            {

                HashedPassword loginUserHash = await encryptionService.EncryptAsync(
                    Encoding.UTF8.GetBytes(user.Password),
                    foundUser.Salt);

                if (Enumerable.SequenceEqual(foundUser.PasswordHash, loginUserHash.Hash))
                    foundUser.Jwt = jwtService.GenerateToken(foundUser);
            }

            return mapper.Map<User, UserTokenDto>(foundUser);
        }

    }
}
