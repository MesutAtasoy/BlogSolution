using BlogSolution.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Stats.Api.IntegrationEvents.Events;
using Stats.Application.Repositories;
using System.Threading.Tasks;

namespace Stats.Api.IntegrationEvents.EventHandlers
{
    public class PostDeletedIntegrationEventHandler : IIntegrationEventHandler<PostDeletedIntegrationEvent>
    {
        private readonly IBlogStatsItemRepository _blogStatsItemRepository;
        private readonly ILogger<PostDeletedIntegrationEventHandler> _logger;
        public PostDeletedIntegrationEventHandler(IBlogStatsItemRepository blogStatsItemRepository,
            ILogger<PostDeletedIntegrationEventHandler> logger)
        {
            _blogStatsItemRepository = blogStatsItemRepository;
            _logger = logger;

        }

        public async Task Handle(PostDeletedIntegrationEvent @event)
        {
            _logger.LogInformation($"{@event.Id} (Post - {@event.Id} succesfully subscribed)");
            await _blogStatsItemRepository.DeletePost(@event.PostId);
            _logger.LogInformation($"{@event.Id} (Post - {@event.Id} succesfully finished)");
        }
    }
}
