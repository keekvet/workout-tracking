using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Enums;

namespace WorkoutTracking.Application.Models
{
    public class FriendRequestStateUpdateModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public FriendRequestState State { get; set; }
    }
}
