using System.Linq;

namespace PampaDevs.BuildingBlocks.Domain.Repositories
{
    public interface IQueryRepository<TEntity, TId> where TEntity : IAggregateRoot<TId>
    {
        IQueryable<TEntity> Queryable();
    }
}
