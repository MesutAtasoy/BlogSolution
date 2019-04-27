using BlogSolution.Types;
using Identity.Application.Contracts;
using Identity.Domain.Models;
using Identity.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;
using BlogSolution.Types.Exceptions;

namespace Identity.Application.Services
{
    public class UserPasswordService : IUserPasswordService
    {
        private readonly IdentityDbContext _identityDbContext;
        public UserPasswordService(IdentityDbContext identityDbContext) => _identityDbContext = identityDbContext;

        public IQueryable<UserPasswordRequest> FindBy(Guid userId) =>
            _identityDbContext.UserPasswordRequests.Where(c => c.UserId == userId).AsQueryable();

        public async Task<UserPasswordRequest> AddUserRequestAsync(UserPasswordRequest userPasswordRequest)
        {
            await _identityDbContext.UserPasswordRequests.AddAsync(userPasswordRequest);

            if (await _identityDbContext.SaveChangesAsync() > 0)
                return userPasswordRequest;

            throw new BlogSolutionException("An error has occured while the user is adding", ApplicationStatusCode.AnErrorHasOccured);
        }

        public void UpdateUserPassword(UserPasswordRequest userPasswordRequest)
        {
            _identityDbContext.Entry(userPasswordRequest).State = EntityState.Modified;
            _identityDbContext.SaveChanges();
        }
    }
}
