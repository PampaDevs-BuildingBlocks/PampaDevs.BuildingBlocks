using System.Threading.Tasks;

namespace PampaDevs.BuildingBlocks.Domain.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : IAggregateRoot<TId>
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        int Delete(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
    }
}
