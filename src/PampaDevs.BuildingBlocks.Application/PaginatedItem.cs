using System;
using System.Collections.Generic;
using System.Text;

namespace PampaDevs.BuildingBlocks.Application
{
    /// <summary>
    /// Paginated Item response
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public class PaginatedItem<TResponse> : IDto
    {
        public PaginatedItem(long totalItems, long totalPages, IReadOnlyList<TResponse> items)
        {
            TotalItems = totalItems;
            TotalPages = totalPages;
            Items = items;
        }

        public long TotalItems { get; }
        public long TotalPages { get; }
        public IReadOnlyList<TResponse> Items { get; }
    }
}
