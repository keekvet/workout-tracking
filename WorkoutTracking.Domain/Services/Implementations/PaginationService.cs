using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models.Pagination.Base;
using WorkoutTracking.Application.Services.Interfaces;
using WorkoutTracking.Data.Repositories;

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

        public async Task<IEnumerable<TDto>> GetRangeAsync(
            SortedPaginationModel model,
            Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = repository.GetAll();

            if (filter is not null)
                query = query.Where(filter);

            if (model.PropertyToSort is not null)
            {
                Type type = typeof(TEntity);

                PropertyDescriptor sortProperty =
                    TypeDescriptor.GetProperties(typeof(TEntity)).Find(model.PropertyToSort, true);

                if (sortProperty is not null)
                {
                    ParameterExpression paramExpr = Expression.Parameter(type);

                    Expression<Func<TEntity, object>> expression = Expression.Lambda<Func<TEntity, object>>(
                        Expression.Convert(
                            Expression.Property(paramExpr, sortProperty.Name),
                            typeof(object)), paramExpr);

                    query = model.SortByAscending ?
                        query.OrderBy(expression) :
                        query.OrderByDescending(expression);
                }
            }

            return await query
                .Skip(model.Offset.Value)
                .Take(model.Count.Value)
                .Select(e => mapper.Map<TEntity, TDto>(e))
                .ToListAsync();
        }

        public IEnumerable<TDto> MakePage(
            SortedPaginationModel model,
            IEnumerable<TEntity> source)
        {
            if (model.PropertyToSort is not null)
            {
                PropertyDescriptor sortProperty =
                    TypeDescriptor.GetProperties(typeof(TEntity)).Find(model.PropertyToSort, true);

                if (sortProperty is null)
                    return null;

                source = model.SortByAscending ?
                    source.OrderBy(x => sortProperty) :
                    source.OrderByDescending(x => sortProperty);
            }

            return source
            .Skip(model.Offset.Value)
            .Take(model.Count.Value)
            .Select(e => mapper.Map<TEntity, TDto>(e));
        }
    }
}
