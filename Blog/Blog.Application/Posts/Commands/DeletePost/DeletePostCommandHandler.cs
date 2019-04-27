using AutoMapper;
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
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeletePostCommandHandler(BlogDbContext context, IMediator mediator, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
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

            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success);
        }
    }
}
