using BlogSolution.Framework.Types;
using MediatR;
using System;

namespace Blog.Application.Categories.Queries
{
    public class GetCategoryQuery : IRequest<ApiBaseResponse>
    {
        public Guid? CategoryId { get; set; }
        public string Name { get; set; }
    }
}
