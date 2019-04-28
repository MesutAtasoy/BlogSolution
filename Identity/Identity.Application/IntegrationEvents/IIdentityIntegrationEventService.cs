using BlogSolution.EventBus.Events;

namespace Identity.Application.IntegrationEvents
{
    public interface IIdentityIntegrationEventService
    {
        void PublishEventBusAsync(IntegrationEvent @event);
    }
}
