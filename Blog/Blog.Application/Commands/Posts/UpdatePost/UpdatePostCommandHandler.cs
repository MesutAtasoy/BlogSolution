using AutoMapper;
using Blog.Application.Dtos;
using Blog.Persistance;
using BlogSolution.Shared.Helpers;
using BlogSolution.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Posts.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ApiBaseResponse> , ICommand
    {
        private readonly BlogDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(BlogDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(c => c.Id == request.PostId, cancellationToken);
            if (post == null)
                return new ApiBaseResponse(HttpStatusCode.NotFound, ApplicationStatusCode.AnErrorHasOccured, null, "Post is not found");

            var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);

            if (userId != post.AuthorId)
                return new ApiBaseResponse(HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "Unauthorized Author");

            post.Update(request.Title, UrlHelper.GetFriendlyTitle(request.Title), request.PostContent, request.CategoryId, userId);
            
            await _context.SaveChangesAsync(cancellationToken);
            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success, _mapper.Map<PostDto>(post));
        }
    }
}
