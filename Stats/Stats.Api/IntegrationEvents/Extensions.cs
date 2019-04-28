using Microsoft.Extensions.DependencyInjection;
using Stats.Api.IntegrationEvents.EventHandlers;

namespace Stats.Api.IntegrationEvents
{
    public static class Extensions
    {
        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            services.AddTransient<PostDeletedIntegrationEventHandler>();
            return services;
        }
    }
}
