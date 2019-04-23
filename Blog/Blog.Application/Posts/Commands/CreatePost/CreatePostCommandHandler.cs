using AutoMapper;
using Blog.Application.Dtos;
using Blog.Domain.Models;
using Blog.Persistance;
using BlogSolution.Framework.Helpers;
using BlogSolution.Framework.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ApiBaseResponse>, ICommand
    {
        private readonly BlogDbContext _context;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public CreatePostCommandHandler(BlogDbContext context, IMediator mediator, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);

            if (userId != request.AuthorId)
                return new ApiBaseResponse(HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "Unauthorized Author");

            var entity = new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = request.CategoryId,
                Title = request.Title,
                FriendlyTitle = UrlHelper.GetFriendlyTitle(request.Title),
                AuthorId = userId,
                CreatedBy = userId,
                PostContent = request.PostContent,
                IsActive = true
            };

            if (request.Tags.Count > 0)
            {
                foreach (var tag in request.Tags)
                {
                    Tag existingTag = _context.Tags.SingleOrDefault(c => c.Name == tag.ToLowerInvariant());
                    if (existingTag == null)
                    {
                        entity.PostTags.Add(new PostTag
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
                        entity.PostTags.Add(new PostTag
                        {
                            Tag = existingTag
                        });
                    }
                }
            }

            _context.Posts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);


            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success, _mapper.Map<PostDto>(entity));
        }
    }
}
