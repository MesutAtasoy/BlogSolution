using BlogSolution.Mongo;
using BlogSolution.Types;
using Microsoft.AspNetCore.Http;
using Stats.Application.Models;
using Stats.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Stats.Application.Repositories
{
    public class BlogStatsItemRepository : IBlogStatsItemRepository
    {
        private readonly IMongoRepository<BlogStatsItem> _blogStatsItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogStatsItemRepository(IMongoRepository<BlogStatsItem> blogStatsItemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blogStatsItemRepository = blogStatsItemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<BlogStatsItem>> GetStatsByBlogId(Guid postId)
        {
            var statsItems = await _blogStatsItemRepository.FindAsync(c => c.PostId == postId);
            return statsItems.ToList();
        }
        public async Task<ApiBaseResponse> CommentPostAsync(CommentRequestModel comment)
        {
            var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);

            var statsItem =  await _blogStatsItemRepository.GetAsync(c => c.PostId == comment.PostId);
            if (statsItem != null)
            {
                statsItem.AddComment(comment.Comment,userId);
                await _blogStatsItemRepository.UpdateAsync(statsItem);
            }
            else
            {
                statsItem = new BlogStatsItem(comment.PostId);
                statsItem.AddComment(comment.Comment,userId);
                await _blogStatsItemRepository.AddAsync(statsItem);
            }
            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success, null, "Successfully commented.");
        }
        public async Task<ApiBaseResponse> FavoritePostAsync(CommentRequestModel comment)
        {
            var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);

            var statsItem =  await _blogStatsItemRepository.GetAsync(c => c.PostId == comment.PostId);
            if (statsItem != null)
            {
                statsItem.AddFavorite(userId);
                await _blogStatsItemRepository.UpdateAsync(statsItem);
            }
            else
            {
                statsItem = new BlogStatsItem(comment.PostId);
                statsItem.AddFavorite(userId);
                await _blogStatsItemRepository.AddAsync(statsItem);
            }
            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success, null, "Successfully favorited.");
        }

        public async Task DeletePost(Guid postId)
        {
            var item = await _blogStatsItemRepository.GetAsync(c=>c.PostId == postId);
            if(item!=null)
                await _blogStatsItemRepository.DeleteAsync(item.Id);
        }
    }
}
