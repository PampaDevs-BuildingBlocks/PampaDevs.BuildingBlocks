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
            DateCreated = NewDateTime();

            ValidateCreation();
        }

        [Key] public TId Id { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public DateTime? DateUpdated { get; protected set; }

        protected abstract void ValidateCreation();
    }
}
