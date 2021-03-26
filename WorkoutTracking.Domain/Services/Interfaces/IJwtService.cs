using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Domain.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
