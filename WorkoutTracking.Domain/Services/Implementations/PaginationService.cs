using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Repositories;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.Models.Pagination.Base;
using System.Reflection;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class PaginationService<TEntity, TDto> : IPaginationService<TEntity, TDto>
        where TEntity : class where TDto : class
    {
        private readonly IRepository<TEntity> repository;
        private readonly IMapper mapper;

        public PaginationService(IRepository<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ICollection<TDto>> GetRangeAsync(SortedPaginationModel model, Func<TEntity, bool> filter)
        {
            if (model.Count is null
                || model.Offset is null
                || model.Count <= 0
                || model.Offset < 0)
                return null;

            IQueryable<TEntity> query = repository.GetAll();

            if (filter is not null)
                query = query.Where(filter).AsQueryable();

            if (model.PropertyToSort is not null) 
            {
                Func<TEntity, PropertyInfo> orderFunc = u => u.GetType().GetProperty(model.PropertyToSort);
                IOrderedEnumerable<TEntity> orderedQuery = 
                    model.SortByAscending ? query.OrderBy(orderFunc) : query.OrderByDescending(orderFunc);
                query = orderedQuery.AsQueryable();
            }

            return await query
                .Skip(model.Offset.Value)
                .Take(model.Count.Value)
                .Select(e => mapper.Map<TEntity, TDto>(e))
                .AsQueryable()
                .ToListAsync();
        }
    }
}
