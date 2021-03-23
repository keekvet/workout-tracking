using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Repositories
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
    }
}
