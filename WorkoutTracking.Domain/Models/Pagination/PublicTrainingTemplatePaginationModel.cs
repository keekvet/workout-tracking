using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models.Pagination.Base;

namespace WorkoutTracking.Application.Models.Pagination
{
    public class PublicTrainingTemplatePaginationModel : SortedPaginationModel
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
    }
}
