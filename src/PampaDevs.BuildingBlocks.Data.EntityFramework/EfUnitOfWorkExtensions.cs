using Microsoft.EntityFrameworkCore;
using PampaDevs.BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PampaDevs.BuildingBlocks.Data.EntityFramework
{
    public static class EfUnitOfWorkExtensions
    {
        public static int? GetCommandTimeout(this IEfUnitOfWork uow, DbContext context)
        {
            return context.Database.GetCommandTimeout();
        }

        public static IUnitOfWork SetCommandTimeout(this IEfUnitOfWork uow, DbContext context, int? value)
        {
            context.Database.SetCommandTimeout(value);
            return uow;
        }

        public static int ExecuteSqlCommand(this IEfUnitOfWork uow, DbContext context, string sql,
            params object[] parameters)
        {
            return context.Database.ExecuteSqlRaw(sql, parameters);
        }

        public static async Task<int> ExecuteSqlCommandAsync(this IEfUnitOfWork uow, DbContext context, string sql,
            params object[] parameters)
        {
            return await context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public static async Task<int> ExecuteSqlCommandAsync(this IEfUnitOfWork uow, DbContext context, string sql,
            CancellationToken cancellationToken, params object[] parameters)
        {
            return await context.Database.ExecuteSqlRawAsync(sql, cancellationToken, parameters);
        }
    }
}
