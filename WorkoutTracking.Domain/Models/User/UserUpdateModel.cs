using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Enums;

namespace WorkoutTracking.Application.Models.User
{
    public class UserUpdateModel
    {
        public string Name { get; set; }
        public string About { get; set; }
        public UserAccess Access { get; set; }
    }
}
