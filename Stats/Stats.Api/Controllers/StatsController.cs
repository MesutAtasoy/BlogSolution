using Microsoft.AspNetCore.Mvc;
using Stats.Application.Models;
using Stats.Application.Repositories;
using System;
using System.Threading.Tasks;

namespace Stats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IBlogStatsItemRepository _blogStatsItemRepository;
        public StatsController(IBlogStatsItemRepository blogStatsItemRepository)
        {
            _blogStatsItemRepository = blogStatsItemRepository;
        }

        [HttpPost("Comment")]
        public async Task<IActionResult> Comment([FromBody]CommentRequestModel requestModel) => Ok(await _blogStatsItemRepository.CommentPostAsync(requestModel));

        [HttpPost("Favorite")]
        public async Task<IActionResult> Favorite([FromBody]CommentRequestModel requestModel) => Ok(await _blogStatsItemRepository.FavoritePostAsync(requestModel));

        [HttpPost("GetComments/{Id}")]
        public async Task<IActionResult> GetComments(Guid Id) => Ok(await _blogStatsItemRepository.GetStatsByBlogId(Id));
    }
}
