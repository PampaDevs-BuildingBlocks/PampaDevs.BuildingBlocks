using Microsoft.EntityFrameworkCore;
using PampaDevs.BuildingBlocks.Domain;
using PampaDevs.BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PampaDevs.BuildingBlocks.Data.EfCore
{
    public class Repository<TEntity, TId> : Repository<DbContext, TEntity, TId>, IRepository<TEntity, TId>
        where TEntity : class, IAggregateRoot<TId>
    {
        public Repository(DbContext dbContext) : base(dbContext)
        {

        }
    }

    public class Repository<TDbContext, TEntity, TId> : IRepository<TEntity, TId>
        where TDbContext : DbContext
        where TEntity : class, IAggregateRoot<TId>
    {
        private readonly TDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public int Delete(TEntity entity)
        {
            var entry = _dbSet.Remove(entity);
            return 1;
        }

        public TEntity Update(TEntity entity)
        {
            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            return entry.Entity;
        }
    }
}
