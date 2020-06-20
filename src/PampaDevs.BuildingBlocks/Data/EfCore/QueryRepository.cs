using Microsoft.EntityFrameworkCore;
using PampaDevs.BuildingBlocks.Domain;
using PampaDevs.BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PampaDevs.BuildingBlocks.Data.EfCore
{
    public class QueryRepository<TEntity, TId> : QueryRepository<DbContext, TEntity, TId>
           where TEntity : class, IAggregateRoot<TId>
    {
        public QueryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class QueryRepository<TDbContext, TEntity, TId> : IQueryRepository<TEntity, TId>
        where TDbContext : DbContext
        where TEntity : class, IAggregateRoot<TId>
    {
        private readonly TDbContext _dbContext;

        public QueryRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbContext.Set<TEntity>();
        }
    }
}
