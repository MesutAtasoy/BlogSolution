using Microsoft.Extensions.DependencyInjection;
using Notification.Api.IntegrationEvents.EventHandlers;

namespace Notification.Api.IntegrationEvents
{
    public static class Extensions
    {
        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            services.AddTransient<ForgotPasswordIntegrationEventHandler>();
            return services;
        }
    }
}
