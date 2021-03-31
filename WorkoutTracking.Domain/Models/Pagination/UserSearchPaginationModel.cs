using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models.Pagination.Base;

namespace WorkoutTracking.Application.Models.Pagination
{
    public class UserSearchPaginationModel : SortedPaginationModel
    {
        public string SearchWithName { get; set; }
    }
}
