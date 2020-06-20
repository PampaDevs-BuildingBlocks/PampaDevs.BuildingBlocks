using PampaDevs.Bus;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using static PampaDevs.Utils.Helpers.DateTimeHelper;

namespace PampaDevs.BuildingBlocks.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
    {
        private readonly IDictionary<Type, Action<object>> _handlers = new ConcurrentDictionary<Type, Action<object>>();
        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();
        protected AggregateRoot() : this(default)
        {
        }

        protected AggregateRoot(TId id) : base(id)
        {
            DateCreated = NewDateTime();
        }

        public IAggregateRoot<TId> AddEvent(IEvent uncommittedEvent)
        {
            _uncommittedEvents.Add(uncommittedEvent);
            ApplyEvent(uncommittedEvent);
            return this;
        }

        public IAggregateRoot<TId> ApplyEvent(IEvent payload)
        {
            if (!_handlers.ContainsKey(payload.GetType()))
                return this;
            _handlers[payload.GetType()]?.Invoke(payload);
            return this;
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public List<IEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        public IAggregateRoot<TId> RegisterHandler<T>(Action<T> handler)
        {
            _handlers.Add(typeof(T), e => handler((T)e));
            return this;
        }

        public IAggregateRoot<TId> RemoveEvent(IEvent @event)
        {
            if (_uncommittedEvents.Find(e => e == @event) != null)
                _uncommittedEvents.Remove(@event);
            return this;
        }
    }
}
