using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PampaDevs.Bus;
using PampaDevs.Bus.InProc;
using PampaDevs.Bus.InProc.Notifications;
using PampaDevs.Utils;
using System;

namespace PampaDevs.BuildingBlocks.Bus
{
    public static class Abstractions
    {
        private static bool _initialized = false;
        public static IServiceCollection AddInProcBus(this IServiceCollection services, params Type[] assemblyMarkerTypes)
        {
            Init();

            services.AddMediatR(assemblyMarkerTypes);


            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandValidationHandler<,>));

            services.AddScoped<IDomainDispatcher, InMemoryBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            return services;
        }

        public static IServiceCollection AddInterProcBus(this IServiceCollection services, params Type[] assemblyMarkerTypes)
        {
            Init();

            return services;
        }

        private static void Init()
        {
            Ensure.Not(_initialized, "You cannot initialize the Bus twice");

            _initialized = true;
        }
    }
}