using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Domain.Services.Interfaces
{
    public interface IFollowerService
    {
        Task<User> GetUserFollowerById(User user, User follower);
    }
}
