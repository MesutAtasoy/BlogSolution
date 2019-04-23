using BlogSolution.Framework.Types;
using MediatR;
using System;

namespace Blog.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest<ApiBaseResponse>
    {
        public Guid PostId { get; set; }
    }
}
