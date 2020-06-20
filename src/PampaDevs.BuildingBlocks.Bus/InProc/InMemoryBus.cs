﻿using MediatR;
using System.Threading.Tasks;

namespace PampaDevs.BuildingBlocks.Bus.InProc
{
    public class InMemoryBus : IDomainDispatcher
    {
        private readonly IMediator _mediator;
        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchCommand<T>(T command)
        {
            await _mediator.Send(command);
        }

        public async Task DispatchEvent<T>(T @event)
        {
            await _mediator.Publish(@event);
        }
    }
}
