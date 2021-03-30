﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class FriendRequest
    {
        public int RequestFromId { get; set; }
        public virtual User RequestFrom { get; set; }
        public int RequestToId { get; set; }
        public virtual User RequestTo { get; set; }
        public bool IsRefused { get; set; }
    }
}
