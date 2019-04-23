using Identity.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Application.Contracts
{
    public interface IUserPasswordService
    {
        IQueryable<UserPasswordRequest> FindBy(Guid userId);
        Task<UserPasswordRequest> AddUserRequestAsync(UserPasswordRequest userPasswordRequest);
        void UpdateUserPassword(UserPasswordRequest userPasswordRequest);
    }
}
