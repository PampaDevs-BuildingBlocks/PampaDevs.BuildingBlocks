using System;

namespace PampaDevs.BuildingBlocks.Domain.Events
{
    /// <summary>
    ///  Supertype for all Event types
    /// </summary>
    public interface IEvent
    {
        Guid Id { get; }
        int EventVersion { get; }
        DateTime OccurredOn { get; }
    }
}