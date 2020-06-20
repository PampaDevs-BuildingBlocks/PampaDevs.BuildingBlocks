using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PampaDevs.BuildingBlocks.Application;
using PampaDevs.BuildingBlocks.Domain;
using PampaDevs.BuildingBlocks.Domain.Repositories;
using PampaDevs.BuildingBlocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PampaDevs.BuildingBlocks.Data.EntityFramework
{
    public static class QueryRepositoryExtensions
    {
        public static async Task<TEntity> GetByIdAsync<TDbContext, TEntity, TEntityId>(
          this IQueryRepository<TEntity, TEntityId> repo,
          Guid id,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          bool disableTracking = true)
          where TDbContext : DbContext
          where TEntity : class, IAggregateRoot<TEntityId>
        {
            var queryable = repo.Queryable();

            if (disableTracking) queryable = queryable.AsNoTracking();

            if (include != null) queryable = include.Invoke(queryable);

            return await queryable.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public static async Task<TEntity> FindOneAsync<TDbContext, TEntity, TEntityId>(
            this IQueryRepository<TEntity, TEntityId> repo,
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
            where TDbContext : DbContext
            where TEntity : class, IAggregateRoot<TEntityId>
        {
            var queryable = repo.Queryable();

            if (include != null) queryable = include.Invoke(queryable);

            if (disableTracking) queryable = queryable.AsNoTracking();

            return await queryable.FirstOrDefaultAsync(filter);
        }

        public static async Task<IReadOnlyList<TEntity>> ListAsync<TDbContext, TEntity, TEntityId>(
            this IQueryRepository<TEntity, TEntityId> repo,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
            where TDbContext : DbContext
            where TEntity : class, IAggregateRoot<TEntityId>
        {
            var queryable = repo.Queryable();

            if (include != null) queryable = include.Invoke(queryable);

            if (disableTracking) queryable = queryable.AsNoTracking();

            return await queryable.ToListAsync();
        }

        public static async Task<PaginatedItem<TResponse>> QueryAsync<TDbContext, TEntity, TEntityId, TResponse>(
            this IQueryRepository<TEntity, TEntityId> repo,
            PaginateCriteria paginateCriteria,
            Expression<Func<TEntity, TResponse>> selector,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
            where TDbContext : DbContext
            where TEntity : class, IAggregateRoot<TEntityId>
        {
            return await GetDataAsync<TDbContext, TEntity, TEntityId, TResponse>(repo, paginateCriteria, selector, null, include, disableTracking);
        }

        public static async Task<PaginatedItem<TResponse>> FindAllAsync<TDbContext, TEntity, TEntityId, TResponse>(
            this IQueryRepository<TEntity, TEntityId> repo,
            PaginateCriteria paginateCriteria,
            Expression<Func<TEntity, TResponse>> selector,
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
            where TDbContext : DbContext
            where TEntity : class, IAggregateRoot<TEntityId>
        {
            return await GetDataAsync<TDbContext, TEntity, TEntityId, TResponse>(repo, paginateCriteria, selector, filter, include, disableTracking);
        }

        private static async Task<PaginatedItem<TResponse>> GetDataAsync<TDbContext, TEntity, TEntityId, TResponse>(
            IQueryRepository<TEntity, TEntityId> repo,
            PaginateCriteria paginateCriteria,
            Expression<Func<TEntity, TResponse>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
            where TDbContext : DbContext
            where TEntity : class, IAggregateRoot<TEntityId>
        {
            var queryable = repo.Queryable();

            if (disableTracking) queryable = queryable.AsNoTracking();

            if (include != null) queryable = include.Invoke(queryable);

            if (filter != null) queryable = queryable.Where(filter);

            if (!string.IsNullOrWhiteSpace(paginateCriteria.SortBy))
            {
                var isDesc = string.Equals(paginateCriteria.SortOrder, "desc", StringComparison.OrdinalIgnoreCase) ? true : false;
                queryable = queryable.OrderByPropertyName<TEntity, TEntityId>(paginateCriteria.SortBy, isDesc);
            }

            var results = await queryable
                .Skip(paginateCriteria.CurrentPage * paginateCriteria.PageSize)
                .Take(paginateCriteria.PageSize)
                .Select(selector)
                .ToListAsync();

            var totalRecord = await queryable.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecord / paginateCriteria.PageSize);

            if (paginateCriteria.CurrentPage > totalPages)
            {
                paginateCriteria.SetCurrentPage(totalPages);                
            }

            return new PaginatedItem<TResponse>(totalRecord, totalPages, results);
        }
    }
}
