using AutoMapper;
using Blog.Application.Dtos;
using Blog.Persistance;
using BlogSolution.Types;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Application.Queries.Categories
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ApiBaseResponse>, IQuery
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(BlogDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Categories.AsQueryable();

            if (request.CategoryId.HasValue)
                query = query.Where(c => c.Id == request.CategoryId.Value);

            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(c => c.Name.Contains(request.Name));

            return new ApiBaseResponse(System.Net.HttpStatusCode.OK,ApplicationStatusCode.Success,_mapper.Map<List<CategoryDto>>(await query.ToListAsync(cancellationToken)));

        }
    }
}
