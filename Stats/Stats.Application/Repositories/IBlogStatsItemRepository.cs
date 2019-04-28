using BlogSolution.Types;
using Stats.Application.Models;
using Stats.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stats.Application.Repositories
{
    public interface IBlogStatsItemRepository
    {
        Task<List<BlogStatsItem>> GetStatsByBlogId(Guid postId);
        Task<ApiBaseResponse> CommentPostAsync(CommentRequestModel comment);
        Task<ApiBaseResponse> FavoritePostAsync(CommentRequestModel comment);
        Task DeletePost(Guid postId);
    }
}
