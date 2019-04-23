using AutoMapper;
using Blog.Application.Dtos;
using Blog.Persistance;
using BlogSolution.Framework.Helpers;
using BlogSolution.Framework.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ApiBaseResponse> , ICommand
    {

        private readonly BlogDbContext _context;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(BlogDbContext context, IMediator mediator, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(c => c.Id == request.PostId);
            if (post == null)
                return new ApiBaseResponse(HttpStatusCode.NotFound, ApplicationStatusCode.AnErrorHasOccured, null, "Post is not found");

            var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);

            if (userId != post.AuthorId)
                return new ApiBaseResponse(HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "Unauthorized Author");

            post.CategoryId = request.CategoryId;
            post.PostContent = request.PostContent;
            post.Title = request.Title;
            post.UpdatedBy = userId;
            post.UpdatedDate = DateTime.UtcNow;
            post.FriendlyTitle = UrlHelper.GetFriendlyTitle(request.Title);
            _context.SaveChanges();

            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success, _mapper.Map<PostDto>(post));
        }
    }
}
