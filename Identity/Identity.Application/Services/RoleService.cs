using System.Threading.Tasks;
using Identity.Application.Contracts;
using Identity.Domain.Models;
using Identity.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Services
{
    public class RoleService : IRoleService 
    {
        private readonly IdentityDbContext _identityDbContext;
        public RoleService(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<Role> GetRoleByName(string name)
         => await _identityDbContext.Roles.SingleOrDefaultAsync(x => x.Name == name);
    }
}
