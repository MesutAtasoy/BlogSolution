using Identity.Domain.Models;
using System.Threading.Tasks;

namespace Identity.Application.Contracts
{
    public interface IRoleService
    {
        Task<Role> GetRoleByName(string name);
    }
}
