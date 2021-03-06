using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Context;

namespace WorkoutTracking.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> dbEntities;
        private readonly WorkoutContext context;
        public Repository(WorkoutContext context)
        {
            this.context = context;
            dbEntities = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await dbEntities.AddAsync(entity)).Entity;
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return dbEntities.AddRangeAsync(entities);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            return await Task.Run(() => dbEntities.Remove(entity).State == EntityState.Deleted);
        }

        public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            return Task.Run(() => dbEntities.RemoveRange(entities));
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbEntities.FindAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() => dbEntities.Update(entity).Entity);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, int secondId)
        {
            return await dbEntities.FindAsync(id, secondId);
        }
    }
}
