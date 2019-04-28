using BlogSolution.EventBus.Abstractions;
using Stats.Api.IntegrationEvents.Events;
using Stats.Application.Repositories;
using System.Threading.Tasks;

namespace Stats.Api.IntegrationEvents.EventHandlers
{
    public class PostDeletedIntegrationEventHandler : IIntegrationEventHandler<PostDeletedIntegrationEvent>
    {
        private readonly IBlogStatsItemRepository _blogStatsItemRepository;
        public PostDeletedIntegrationEventHandler(IBlogStatsItemRepository blogStatsItemRepository)
        {
            _blogStatsItemRepository = blogStatsItemRepository;
        }

        public async Task Handle(PostDeletedIntegrationEvent @event)
        {
            await _blogStatsItemRepository.DeletePost(@event.PostId);
        }
    }
}
