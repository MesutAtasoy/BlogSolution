using BlogSolution.Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BlogSolutionAuth("identity-api-key")]
    public class BaseApiController : ControllerBase
    {
    }
}
