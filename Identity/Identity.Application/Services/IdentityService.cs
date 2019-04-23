using System.Threading.Tasks;
using BlogSolution.Framework.Authentication.Handlers;
using BlogSolution.Framework.Password;
using BlogSolution.Framework.Types;
using BlogSolution.Framework.Exceptions;
using Identity.Application.Contracts;
using Identity.Application.Models;
using Identity.Domain.Models;
using Identity.Application.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Identity.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserService _userService;
        private readonly IUserPasswordService _userPasswordService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IdentitySettings _identitySettings;

        public IdentityService(IUserService userService, IJwtHandler jwtHandler, IOptions<IdentitySettings> options, IHttpContextAccessor httpContextAccessor,IUserPasswordService userPasswordService)
        {
            _userService = userService;
            _userPasswordService = userPasswordService;
            _jwtHandler = jwtHandler;
            _identitySettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiBaseResponse> LoginAsync(LoginRequestModel requestModel)
        {
            ApiBaseResponse apiBaseResponse = new ApiBaseResponse();
            var user = await _userService.FindByEmailAsync(requestModel.Email);
            if (user == null) return new ApiBaseResponse(System.Net.HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "The email or password is wrong");
            if (!user.IsActive || user.IsDeleted) return new ApiBaseResponse(System.Net.HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "The email or password is wrong");

            bool isVerified = PasswordHasher.Verify(user.PasswordSalt, user.PasswordHash, requestModel.Password);
            if (!isVerified) return new ApiBaseResponse(System.Net.HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "The email or password is wrong");

            var token = _jwtHandler.CreateToken(user.Id.ToString());
            apiBaseResponse.Result = token;
            return apiBaseResponse;
        }

        public async Task<ApiBaseResponse> RegisterAsync(RegisterRequestModel requestModel)
        {
            ApiBaseResponse apiBaseResponse = new ApiBaseResponse();

            var isExistedEmail = await _userService.ExistedByEmailAsync(requestModel.Email);
            if (isExistedEmail)
            {
                throw new BlogSolutionException("The email address has already exist.", ApplicationStatusCode.AnErrorHasOccured);
            }

            var isExistedUsername = await _userService.ExistedByUsernameAsync(requestModel.Username);
            if (isExistedUsername)
            {
                throw new BlogSolutionException("The username has already exist.", ApplicationStatusCode.AnErrorHasOccured);
            }

            var password = new PasswordHasher(requestModel.Password);

            await _userService.AddUserAsync(new User
            {
                Username = requestModel.Username,
                Name = requestModel.Name,
                Email = requestModel.Email,
                PhoneNumber = requestModel.PhoneNumber,
                PasswordHash = password.Hash,
                PasswordSalt = password.Salt
            });

            return apiBaseResponse;
        }

        public async Task<ApiBaseResponse> ChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.Identity?.Name);

            var user = await _userService.FindByEmailAsync(requestModel.Email);
            if (user == null) return new ApiBaseResponse(System.Net.HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "The user is not found.");
            if (!user.IsActive || user.IsDeleted) return new ApiBaseResponse(System.Net.HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "The user is not found.");

            if (currentUserId != user.Id)
                return new ApiBaseResponse(System.Net.HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "Unauthorized User to change password.");

            if (_identitySettings.ValidateOldPassword)
            {
                bool isVerified = PasswordHasher.Verify(user.PasswordSalt, user.PasswordHash, requestModel.Password);
                if (!isVerified) return new ApiBaseResponse(System.Net.HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "The email or password is wrong");
            }

            var password = new PasswordHasher(requestModel.NewPassword);

            user.PasswordHash = password.Hash;
            user.PasswordSalt = password.Salt;
            user.UpdatedDate = DateTime.Now;
            user.UpdatedBy = currentUserId;


            _userService.UpdateUser(user);

            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success, null, "The password has succesfully changed.");
        }

        public async Task<ApiBaseResponse> ForgatPasswordAsync(ForgotPasswordRequestModel requestModel)
        {
            

            var user = await _userService.FindByEmailAsync(requestModel.Email);
            if (user == null) return new ApiBaseResponse(System.Net.HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "The user is not found.");
            if (!user.IsActive || user.IsDeleted) return new ApiBaseResponse(System.Net.HttpStatusCode.BadRequest, ApplicationStatusCode.AnErrorHasOccured, null, "The user is not found.");

          

            var oldPasswordRequests =  _userPasswordService.FindBy(user.Id).Where(c=>c.IsActive == true);

            foreach (var oldPasswordRequest in oldPasswordRequests)
            {
                oldPasswordRequest.IsActive = false;
                oldPasswordRequest.UpdatedDate = DateTime.Now;
                oldPasswordRequest.UpdatedBy = user.Id;
                _userPasswordService.UpdateUserPassword(oldPasswordRequest);
            }

            Guid activationCode = Guid.NewGuid();

            DateTime now = DateTime.UtcNow;
            var userRequest = await _userPasswordService.AddUserRequestAsync(new UserPasswordRequest
            {
                UserId = user.Id,
                ActivationCode = activationCode,
                ExpiredDate = now.AddHours(_identitySettings.ActivationLinkLifeTime),
                CreatedBy = user.Id,
                CreatedDate = now,
                IsActive = true,
            });

            // ToDo Notification Api Implementation. Message Handler

            return new ApiBaseResponse(HttpStatusCode.OK, ApplicationStatusCode.Success, null, "The mail succesfully is sent.");
        }

    }
}
