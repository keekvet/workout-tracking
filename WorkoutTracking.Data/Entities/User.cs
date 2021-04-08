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
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public string Jwt { get; set; }
        public string About { get; set; }
        public UserAccess Access { get; set; }
        public virtual TrainingTemplate ActiveTraining { get; set; }
        public virtual ICollection<User> FriendsTo { get; set; } = new List<User>();
        public virtual ICollection<User> FriendsFrom { get; set; } = new List<User>();
        public virtual ICollection<FriendRequest> SendedFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual ICollection<FriendRequest> ReceivedFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<User> Following { get; set; }
        public virtual ICollection<TrainingHistory> TrainingHistory { get; set; } = new List<TrainingHistory>();
        public virtual ICollection<TrainingTemplate> TrainingTemplates { get; set; } = new List<TrainingTemplate>();
    }
}
