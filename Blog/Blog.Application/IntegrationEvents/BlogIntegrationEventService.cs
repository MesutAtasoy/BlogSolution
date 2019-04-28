using BlogSolution.EventBus.Abstractions;
using BlogSolution.EventBus.Events;

namespace Blog.Application.IntegrationEvents
{
    public class BlogIntegrationEventService : IBlogIntegrationEventService
    {
        private readonly IEventBus _eventBus;

        public BlogIntegrationEventService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void PublishEventBusAsync(IntegrationEvent @event)
        {
            _eventBus.Publish(@event);
        }
    }
}
