using BlogSolution.Types;
using MediatR;
using System;
using System.Collections.Generic;

namespace Blog.Application.Commands.Posts.UpdatePost
{
    public class UpdatePostCommand : IRequest<ApiBaseResponse>
    {
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
    }
}
