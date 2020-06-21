using System.Threading.Tasks;

namespace PampaDevs.Bus
{
    public interface IDomainDispatcher
    {
        Task<object> DispatchCommand<T>(T command);
        Task DispatchEvent<T>(T @event);
    }
}
