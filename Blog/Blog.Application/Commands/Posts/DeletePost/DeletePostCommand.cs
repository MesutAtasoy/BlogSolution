using BlogSolution.Types;
using MediatR;
using System;

namespace Blog.Application.Commands.Posts.DeletePost
{
    public class DeletePostCommand : IRequest<ApiBaseResponse>
    {
        public Guid PostId { get; set; }
    }
}
