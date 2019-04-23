using Blog.Application.Posts.Commands.CreatePost;
using Blog.Application.Posts.Commands.DeletePost;
using Blog.Application.Posts.Commands.UpdatePost;
using Blog.Application.Posts.Queries;
using Identity.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetPosts([FromQuery] GetPostQuery query) => Ok(await _mediator.Send(query));

        [HttpPost("NewPost")]
        public async Task<IActionResult> NewPost([FromBody]CreatePostCommand command) => Ok(await _mediator.Send(command));
        
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdatePostCommand command) => Ok(await _mediator.Send(command));

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeletePostCommand command) => Ok(await _mediator.Send(command));
    }
}