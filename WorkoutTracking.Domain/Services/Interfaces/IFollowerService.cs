﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IFollowerService
    {
        Task<User> GetUserFollowerByIdAsync(User user, User follower);
    }
}
