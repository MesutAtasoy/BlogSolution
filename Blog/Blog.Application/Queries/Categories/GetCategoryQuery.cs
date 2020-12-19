using BlogSolution.Types;
using MediatR;
using System;

namespace Blog.Application.Queries.Categories
{
    public class GetCategoryQuery : IRequest<ApiBaseResponse>
    {
        public Guid? CategoryId { get; set; }
        public string Name { get; set; }
    }
}
