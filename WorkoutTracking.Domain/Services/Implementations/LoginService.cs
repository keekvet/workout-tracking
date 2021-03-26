using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Domain.Services.Interfaces;

namespace WorkoutTracking.Domain.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IUserService userService;

        public LoginService(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<User> LoginAsync(User user)
        {
            User foundUser = await userService.GetUserByNameAsync(user.Name);
            
            if (foundUser is null)
                return null;

            throw new NotImplementedException();
            //if(user)
        }
    }
}
