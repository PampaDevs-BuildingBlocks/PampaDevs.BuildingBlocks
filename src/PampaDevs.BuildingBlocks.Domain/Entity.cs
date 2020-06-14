using System;
using System.ComponentModel.DataAnnotations;

namespace PampaDevs.BuildingBlocks.Domain
{
    public abstract class Entity<TId> : IEntity<TId>
    {
        protected Entity(TId id)
        {
            Id = id;
            Created = DateTimeOffset.Now.UtcDateTime;
        }
        [Key] public TId Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime? Updated { get; protected set; }
    }
}
