using System.Threading.Tasks;

namespace PampaDevs.BuildingBlocks.Domain.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : IAggregateRoot<TId>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
    }
}
