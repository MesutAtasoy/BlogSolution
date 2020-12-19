using System.Threading.Tasks;
using Blog.Application.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories([FromQuery] GetCategoryQuery query) => Ok(await Mediator.Send(query));
    }
}