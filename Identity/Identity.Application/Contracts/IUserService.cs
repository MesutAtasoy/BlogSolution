using Identity.Domain.Models;
using System.Threading.Tasks;

namespace Identity.Application.Contracts
{
    public interface IUserService
    {
        Task<User> FindByEmailAsync(string email);
        Task<bool> ExistedByEmailAsync(string email);
        Task<User> FindByUsernameAsync(string username);
        Task<bool> ExistedByUsernameAsync(string username);
        Task<User> AddUserAsync(User user);
        void UpdateUser(User user);
    }
}
