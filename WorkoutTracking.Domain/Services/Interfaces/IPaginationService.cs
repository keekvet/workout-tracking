using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models.Pagination.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IPaginationService<TEntity, TDto>
    {
        Task<ICollection<TDto>> GetRangeAsync(
            SortedPaginationModel model, 
            Expression<Func<TEntity, bool>> filter = null);
    }
}
