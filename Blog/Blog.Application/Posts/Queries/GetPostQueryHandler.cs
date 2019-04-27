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

namespace Blog.Application.Posts.Queries
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, ApiBaseResponse>, IQuery
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public GetPostQueryHandler(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Posts.AsQueryable();

            if (request.CategoryId.HasValue)
                query = query.Where(c => c.Id == request.CategoryId.Value);

            if (request.AuthorId.HasValue)
                query = query.Where(c => c.Id == request.AuthorId.Value);

            if (!string.IsNullOrEmpty(request.Title))
                query = query.Where(c => c.Title.Contains(request.Title));


            return new ApiBaseResponse(System.Net.HttpStatusCode.OK, ApplicationStatusCode.Success, _mapper.Map<List<PostDto>>(await query.ToListAsync()));

        }
    }
}
