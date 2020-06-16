using System;
using System.Collections.Generic;

namespace PampaDevs.BuildingBlocks.Domain
{
    public abstract class Identity<TId> : IEquatable<Identity<TId>>, IIdentity<TId>
    {
        public TId Id { get; protected set; }

        protected Identity()
        {
            Id = Id;
        }

        public bool Equals(Identity<TId> other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (ReferenceEquals(null, other)) 
                return false;

            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Identity<TId>);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() ^ 93 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
