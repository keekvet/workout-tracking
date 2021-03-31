using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models.Pagination.Base;

namespace WorkoutTracking.Application.Models.Pagination
{
    public class FriendPaginationModel : SortedPaginationModel
    {
        public int UserId { get; set; }
    }
}
