using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.Application.Commands.Posts.CreatePost;
using Blog.Application.Commands.Posts.DeletePost;
using Blog.Application.Commands.Posts.UpdatePost;
using Blog.Application.Queries.Posts;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseApiController
    {
        public PostController(IMediator mediator) : base(mediator)
        {
        }
               
        [HttpGet("GetPosts")]
        public async Task<IActionResult> GetPosts([FromQuery] GetPostQuery query) => Ok(await Mediator.Send(query));

        [HttpPost("NewPost")]
        public async Task<IActionResult> NewPost([FromBody]CreatePostCommand command) => Ok(await Mediator.Send(command));
        
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdatePostCommand command) => Ok(await Mediator.Send(command));

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeletePostCommand command) => Ok(await Mediator.Send(command));
    }
}