using BlogSolution.Types;
using BlogSolution.Types.Exceptions;
using Identity.Application.Contracts;
using Identity.Domain.Models;
using Identity.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IdentityDbContext _identityDbContext;
        public UserService(IdentityDbContext identityDbContext) => _identityDbContext = identityDbContext;

        public async Task<User> FindByEmailAsync(string email) =>
            await _identityDbContext.Users.SingleOrDefaultAsync(x => x.Email == email.ToLowerInvariant());

        public async Task<User> FindByUsernameAsync(string username) =>
            await _identityDbContext.Users.SingleOrDefaultAsync(x => x.Username == username.ToLowerInvariant());

        public async Task<bool> ExistedByUsernameAsync(string username) =>
            await _identityDbContext.Users.AnyAsync(x => x.Username == username.ToLowerInvariant());

        public async Task<bool> ExistedByEmailAsync(string email) =>
            await _identityDbContext.Users.AnyAsync(x => x.Email == email.ToLowerInvariant());

        public async Task<User> AddUserAsync(User user)
        {
            await _identityDbContext.Users.AddAsync(user);

            if (await _identityDbContext.SaveChangesAsync() > 0)
                return user;

            throw new BlogSolutionException("An error has occured while the user is adding", ApplicationStatusCode.AnErrorHasOccured);
        }

        public void UpdateUser(User user)
        {
            _identityDbContext.Entry(user).State = EntityState.Modified;
            _identityDbContext.SaveChanges();
        }
    }
}
