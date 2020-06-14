using System;
using System.Threading;
using System.Threading.Tasks;

namespace PampaDevs.BuildingBlocks.Domain.Repositories
{
    public interface IUnitOfWork : IRepositoryFactory, IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
