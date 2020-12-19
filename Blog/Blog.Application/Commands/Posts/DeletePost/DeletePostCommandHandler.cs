using AutoMapper;
using Blog.Application.IntegrationEvents;
using Blog.Application.IntegrationEvents.Events;
using Blog.Persistance;
using BlogSolution.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Posts.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ApiBaseResponse>, ICommand
    {

        private readonly BlogDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBlogIntegrationEventService _blogIntegrationEventService;
        private readonly ILogger<DeletePostCommandHandler> _logger;

        public DeletePostCommandHandler(BlogDbContext context, 
            IHttpContextAccessor httpContextAccessor,
            IBlogIntegrationEventService blogIntegrationEventService,
            ILogger<DeletePostCommandHandler> logger
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _blogIntegrationEventService = blogIntegrationEventService;
            _logger = logger;
        }

        public async Task<ApiBaseResponse> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(c => c.Id == request.PostId, cancellationToken);
            if (post == null)
                return new ApiBaseResponse(HttpStatusCode.NotFound, ApplicationStatusCode.AnErrorHasOccured, null, "Post is not found");

            var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);

            if (userId != post.AuthorId)
                return new ApiBaseResponse(HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "Unauthorized Author");

            post.MarkAsDelete(userId);
            await _context.SaveChangesAsync(cancellationToken);

            var @event = new PostDeletedIntegrationEvent(post.Id);
            _logger.LogInformation($"{post.Title} - Delete Handle (Before Publish Event)");
            _blogIntegrationEventService.PublishEventBusAsync(@event);
            _logger.LogInformation($"{post.Title} - Delete Handle (After Publish Event)");
            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success);
        }
    }
}
