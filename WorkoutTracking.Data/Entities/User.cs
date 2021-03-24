using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Enums;

namespace WorkoutTracking.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public UserAccess Access { get; set; }
        public ICollection<User> FriendsTo { get; set; }
        public ICollection<User> FriendsFrom { get; set; }
        public ICollection<FriendRequest> SendedFriendRequests { get; set; }
        public ICollection<FriendRequest> ReceivedFriendRequests { get; set; }
        public ICollection<User> Followers { get; set; }
        public ICollection<User> Following { get; set; }
        public ICollection<TrainingHistory> TrainingHistory { get; set; }
        public ICollection<TrainingTemplate> TrainingTemplates { get; set; }
        public ICollection<PublicTrainingTemplate> PublicTrainingTemplates{ get; set; }
    }
}
