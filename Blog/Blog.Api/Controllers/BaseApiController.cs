using BlogSolution.Authentication.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BlogSolutionAuth("identity-api-key")]
    public class BaseApiController : ControllerBase
    {
        protected IMediator _mediator;
        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
