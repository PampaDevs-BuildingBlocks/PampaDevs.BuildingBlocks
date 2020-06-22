using MediatR;
using System.Threading.Tasks;

namespace PampaDevs.Bus.InProc
{
    public class InMemoryBus : IDomainDispatcher
    {
        private readonly IMediator _mediator;
        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<R> DispatchCommand<T, R>(T command)
        {
            return (R)await _mediator.Send(command);
        }

        public async Task DispatchEvent<T>(T @event)
        {
            await _mediator.Publish(@event);
        }
    }
}