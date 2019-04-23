using BlogSolution.Framework.Mongo;
using BlogSolution.Framework.Types;
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

            BlogStatsItem statsItem = new BlogStatsItem();
            if (await _blogStatsItemRepository.ExistsAsync(c => c.PostId == comment.PostId))
            {

                statsItem = await _blogStatsItemRepository.GetAsync(c => c.PostId == comment.PostId);
                statsItem.Comments.Add(new Comment
                {
                    UserId = userId,
                    CommentText = comment.Comment,
                    CreateDate = DateTime.UtcNow
                });
                await _blogStatsItemRepository.UpdateAsync(statsItem);
            }
            else
            {
                statsItem = new BlogStatsItem
                {
                    PostId = comment.PostId
                };
                statsItem.Comments.Add(new Comment
                {
                    UserId = userId,
                    CommentText = comment.Comment,
                    CreateDate = DateTime.UtcNow
                });
                await _blogStatsItemRepository.AddAsync(statsItem);
            }
            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success, null, "Succesfully commented.");
        }
        public async Task<ApiBaseResponse> FavoritePostAsync(CommentRequestModel comment)
        {
            var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);

            BlogStatsItem statsItem = new BlogStatsItem();
            if (await _blogStatsItemRepository.ExistsAsync(c => c.PostId == comment.PostId))
            {

                statsItem = await _blogStatsItemRepository.GetAsync(c => c.PostId == comment.PostId);
                statsItem.Favorites.Add(new Favorite { Id = Guid.NewGuid(), UserId = userId });
                await _blogStatsItemRepository.UpdateAsync(statsItem);
            }
            else
            {
                statsItem = new BlogStatsItem
                {
                    PostId = comment.PostId
                };
                statsItem.Favorites.Add(new Favorite { Id = Guid.NewGuid(), UserId = userId });
                await _blogStatsItemRepository.AddAsync(statsItem);
            }
            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success, null, "Succesfully favorited.");
        }
    }
}
