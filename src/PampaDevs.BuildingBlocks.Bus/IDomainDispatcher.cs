using System.Threading.Tasks;

namespace PampaDevs.BuildingBlocks.Bus
{
    public interface IDomainDispatcher
    {
        Task DispatchCommand<T>(T command);
        Task DispatchEvent<T>(T @event);
    }
}
