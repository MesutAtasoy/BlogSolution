using BlogSolution.EventBus.Events;

namespace Blog.Application.IntegrationEvents
{
    public interface IBlogIntegrationEventService
    {
        void PublishEventBusAsync(IntegrationEvent @event);
    }
}
