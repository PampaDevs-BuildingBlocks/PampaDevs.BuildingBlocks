using System;
using System.ComponentModel.DataAnnotations;
using static PampaDevs.Utils.Helpers.DateTimeHelper;

namespace PampaDevs.BuildingBlocks.Domain
{
    public abstract class Entity<TId> : IEntity<TId>
    {
        protected Entity(TId id)
        {
            Id = id;
            Created = NewDateTime();
        }
        [Key] public TId Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime? Updated { get; protected set; }
    }
}
