using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Application.Services.Interfaces;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class FriendService : IFriendService
    {
        public Task<FriendRequest> AcceptFriendRequestAsync(User sender, User receiver)
        {
            throw new NotImplementedException();
        }

        public Task<FriendRequest> RefuseFriendRequestAsync(User sender, User receiver)
        {
            throw new NotImplementedException();
        }

        public Task<FriendRequest> SendFriendRequestAsync(User sender, User receiver)
        {
            throw new NotImplementedException();
        }
    }
}
