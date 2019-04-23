using BlogSolution.Framework.Types;
using Identity.Application.Models;
using System.Threading.Tasks;

namespace Identity.Application.Contracts
{
    public interface IIdentityService
    {
        Task<ApiBaseResponse> RegisterAsync(RegisterRequestModel requestModel);
        Task<ApiBaseResponse> LoginAsync(LoginRequestModel requestModel);
        Task<ApiBaseResponse> ChangePasswordAsync(ChangePasswordRequestModel requestModel);
        Task<ApiBaseResponse> ForgatPasswordAsync(ForgotPasswordRequestModel requestModel);
    }
}
