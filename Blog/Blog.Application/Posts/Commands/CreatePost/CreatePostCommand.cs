using BlogSolution.Types;
using MediatR;
using System;
using System.Collections.Generic;

namespace Blog.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<ApiBaseResponse>
    {
        public CreatePostCommand() => Tags = new List<string>();
        public Guid CategoryId { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public List<string> Tags { get; set; }
    }
}
