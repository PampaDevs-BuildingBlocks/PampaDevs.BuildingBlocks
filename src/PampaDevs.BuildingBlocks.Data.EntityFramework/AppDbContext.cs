﻿using Microsoft.EntityFrameworkCore;
using PampaDevs.Bus;
using PampaDevs.BuildingBlocks.Domain;
using PampaDevs.Utils.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReflectionMagic;

namespace PampaDevs.BuildingBlocks.Data.EntityFramework
{
    public abstract class AppDbContext : DbContext
    {
        private readonly IEnumerable<IDomainDispatcher> _eventBuses = null;

        protected AppDbContext(DbContextOptions options, IEnumerable<IDomainDispatcher> eventBuses = null)
            : base(options)
        {
            _eventBuses = eventBuses;
        }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();

            if (result > 0)
            {
                PublishEntityEvents();
            }

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                PublishEntityEvents();
            }

            return result;
        }

        /// <summary>
        /// Source: https://github.com/ardalis/CleanArchitecture/blob/master/src/CleanArchitecture.Infrastructure/Data/AppDbContext.cs
        /// </summary>
        private void PublishEntityEvents()
        {
            var domainEntities = ChangeTracker
                .Entries()
                .Select(e => e.Entity)
                .Where(e => e.GetType().BaseType.IsGenericType && e.GetType().BaseType.GetGenericTypeDefinition().IsAssignableFrom(typeof(AggregateRoot<>)))
                .Where(e => e.AsDynamic().GetUncommittedEvents().Count > 0);

            foreach(var entity in domainEntities)
            {
                var aggregator = entity.AsDynamic();
                var @events = aggregator.GetUncommittedEvents() as List<IEvent>;

                foreach(var @event in @events)
                {
                    _eventBuses.Select(async bus => await bus.DispatchEvent(@event));
                }

                aggregator.ClearUncommittedEvents();
            }
        }
    }
}
