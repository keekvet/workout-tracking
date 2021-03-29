using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Application.Models.Pagination.Base
{
    public class SortedPaginationModel : PaginationModel
    {
        public string PropertyToSort { get; set; }
        public bool SortByAscending { get; set; } = true;
    }
}
