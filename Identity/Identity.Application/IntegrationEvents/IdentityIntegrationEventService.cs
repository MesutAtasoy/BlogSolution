using BlogSolution.EventBus.Abstractions;
using BlogSolution.EventBus.Events;

namespace Identity.Application.IntegrationEvents
{
    public class IdentityIntegrationEventService : IIdentityIntegrationEventService
    {
        private readonly IEventBus _eventBus;

        public IdentityIntegrationEventService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void PublishEventBusAsync(IntegrationEvent @event)
        {
            _eventBus.Publish(@event);
        }
    }
}
