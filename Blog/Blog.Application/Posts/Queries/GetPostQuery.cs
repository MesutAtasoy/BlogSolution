using BlogSolution.Framework.Types;
using MediatR;
using System;

namespace Blog.Application.Posts.Queries
{
    public class GetPostQuery : IRequest<ApiBaseResponse>
    {
        public Guid? CategoryId { get; set; }
        public Guid? AuthorId { get; set; }
        public string Title { get; set; }
    }
}
