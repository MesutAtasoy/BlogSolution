using BlogSolution.Types;
using System.Collections.Generic;

namespace Identity.Domain.Models
{
    public partial class User : EntityBase, ISoftDeletable, IIdentifiable
    {
        public User()
        {
            UserPasswordRequests = new HashSet<UserPasswordRequest>();
            UserRoles = new HashSet<UserRole>();
        }

        public string Username { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<UserPasswordRequest> UserPasswordRequests { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
