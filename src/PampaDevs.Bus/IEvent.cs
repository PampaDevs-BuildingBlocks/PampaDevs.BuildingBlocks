using System;

namespace PampaDevs.Bus
{
    public interface IEvent : IMessage
    {
        public int EventVersion { get; }
        public DateTime OcurredOn { get; }
    }
}
