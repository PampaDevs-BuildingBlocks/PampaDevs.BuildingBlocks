using System;
using static PampaDevs.Utils.Helpers.IdHelper;
using static PampaDevs.Utils.Helpers.DateTimeHelper;

namespace PampaDevs.BuildingBlocks.Domain.Events
{
    public abstract class Event : IEvent
    {
        public Guid Id => NewId();
        public int EventVersion { get; protected set; } = 1;
        public DateTime OccurredOn { get; protected set; } = NewDateTime();
    }
}
