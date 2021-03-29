using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Models.Pagination.Base;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IPaginationService<TEntity, TDto>
    {
        Task<ICollection<TDto>> GetRangeAsync(SortedPaginationModel model, Func<TEntity, bool> filter = null);
    }
}
