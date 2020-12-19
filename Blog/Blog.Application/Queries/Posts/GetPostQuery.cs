using BlogSolution.Types;
using MediatR;
using System;

namespace Blog.Application.Queries.Posts
{
    public class GetPostQuery : IRequest<ApiBaseResponse>
    {
        public Guid? CategoryId { get; set; }
        public Guid? AuthorId { get; set; }
        public string Title { get; set; }
    }
}
