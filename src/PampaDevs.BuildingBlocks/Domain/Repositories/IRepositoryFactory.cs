namespace PampaDevs.BuildingBlocks.Domain.Repositories
{
    public interface IRepositoryFactory
    {
        IQueryRepository<TEntity, TId> QueryRepository<TEntity, TId>() where TEntity : class, IAggregateRoot<TId>;
        IRepository<TEntity, TId> Repository<TEntity, TId>() where TEntity : class, IAggregateRoot<TId>;
    }
}
