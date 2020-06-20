using System;
using System.Threading.Tasks;

namespace PampaDevs.BuildingBlocks.Domain.Events
{
    public interface IDomainEventDispatcher : IDisposable
    {
        Task Dispatch(IEvent @event);
    }
}
