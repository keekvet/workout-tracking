using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Enums;

namespace WorkoutTracking.Application.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public string About { get; set; }
        public UserAccess Access { get; set; }
        public ICollection<User> Friends { get; set; }
        public ICollection<User> Following { get; set; }
        public ICollection<User> Followers { get; set; }
        public ICollection<PublicTrainingTemplate> PublishedTemplates { get; set; }

        // use this if access allow
        public ICollection<TrainingHistory> TrainingHistory { get; set; }
        public ICollection<TrainingTemplate> TrainingTemplates { get; set; }
    }
}
