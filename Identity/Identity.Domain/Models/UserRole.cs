using BlogSolution.Types;
using System;

namespace Identity.Domain.Models
{
    public partial class UserRole : EntityBase, ISoftDeletable, IIdentifiable
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
