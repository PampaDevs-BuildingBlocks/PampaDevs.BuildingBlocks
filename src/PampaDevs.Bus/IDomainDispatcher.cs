using PampaDevs.Bus.InProc;
using System.Threading.Tasks;

namespace PampaDevs.Bus
{
    public interface IDomainDispatcher
    {
        Task<R> DispatchCommand<T, R>(T command);
        Task DispatchEvent<T>(T @event);
    }
}
