using BlogSolution.EventBus.Events;
using System;

namespace Stats.Api.IntegrationEvents.Events
{
    public class PostDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid PostId { get; private set; }

        public PostDeletedIntegrationEvent(Guid postId)
        {
            PostId = postId;
        }
    }
}
