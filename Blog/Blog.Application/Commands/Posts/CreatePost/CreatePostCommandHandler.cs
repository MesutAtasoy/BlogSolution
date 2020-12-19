using AutoMapper;
using Blog.Application.Dtos;
using Blog.Domain.Models;
using Blog.Persistance;
using BlogSolution.Shared.Helpers;
using BlogSolution.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Commands.Posts.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ApiBaseResponse>, ICommand
    {
        private readonly BlogDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(BlogDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);

            if (userId != request.AuthorId)
                return new ApiBaseResponse(HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "Unauthorized Author");

            var post = new Post(request.Title, UrlHelper.GetFriendlyTitle(request.Title), request.PostContent,
                request.AuthorId, request.CategoryId);

            if (request.Tags.Count > 0)
            {
                foreach (var tag in request.Tags)
                {
                    var existingTag = _context.Tags.SingleOrDefault(c => c.Name == tag.ToLowerInvariant());

                    if (existingTag == null)
                    {
                        post.AddPostTag(new PostTag
                        {
                            Tag = new Tag
                            {
                                Name = tag.ToLowerInvariant(),
                                CreatedBy = userId
                            }
                        });
                    }
                    else
                    {
                        post.AddPostTag(new PostTag {Tag = existingTag});
                    }
                }
            }

            await _context.Posts.AddAsync(post, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success, _mapper.Map<PostDto>(post));
        }
    }
}