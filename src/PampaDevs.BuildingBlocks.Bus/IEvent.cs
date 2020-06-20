using System;

namespace PampaDevs.BuildingBlocks.Bus
{
    public interface IEvent : IMessage
    {
        public int EventVersion { get; }
        public DateTime OcurredOn { get; }
    }
}
