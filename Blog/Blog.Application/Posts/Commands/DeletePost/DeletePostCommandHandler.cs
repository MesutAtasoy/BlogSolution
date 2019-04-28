using AutoMapper;
using Blog.Application.IntegrationEvents;
using Blog.Application.IntegrationEvents.Events;
using Blog.Persistance;
using BlogSolution.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ApiBaseResponse>, ICommand
    {

        private readonly BlogDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBlogIntegrationEventService _blogIntegrationEventService;

        public DeletePostCommandHandler(BlogDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, IBlogIntegrationEventService blogIntegrationEventService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _blogIntegrationEventService = blogIntegrationEventService;
        }

        public async Task<ApiBaseResponse> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(c => c.Id == request.PostId);
            if (post == null)
                return new ApiBaseResponse(HttpStatusCode.NotFound, ApplicationStatusCode.AnErrorHasOccured, null, "Post is not found");

            var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);

            if (userId != post.AuthorId)
                return new ApiBaseResponse(HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "Unauthorized Author");

            post.IsDeleted = true;
            post.UpdatedBy = userId;
            post.UpdatedDate = DateTime.UtcNow;
            _context.SaveChanges();

            var @event = new PostDeletedIntegrationEvent(post.Id);
            _blogIntegrationEventService.PublishEventBusAsync(@event);

            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success);
        }
    }
}
