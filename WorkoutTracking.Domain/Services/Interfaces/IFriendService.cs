using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Domain.Services.Interfaces
{
    public interface IFriendService
    {
        Task<FriendRequest> SendFriendRequestAsync(User sender, User receiver);
        Task<FriendRequest> AcceptFriendRequestAsync(User sender, User receiver);
        Task<FriendRequest> RefuseFriendRequestAsync(User sender, User receiver);


    }
}
