using BlogSolution.Authentication.Attributes;
using Identity.Application.Contracts;
using Identity.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IIdentityService _identityService;
        public AccountController(IIdentityService identityService) => _identityService = identityService;

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestModel requestViewModel) => Ok(await _identityService.LoginAsync(requestViewModel));

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestModel requestViewModel) => Ok(await _identityService.RegisterAsync(requestViewModel));

        [BlogSolutionAuth("identity-api-key")]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordRequestModel requestViewModel) => Ok(await _identityService.ChangePasswordAsync(requestViewModel));

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordRequestModel requestViewModel) => Ok(await _identityService.ForgatPasswordAsync(requestViewModel));
    }
}