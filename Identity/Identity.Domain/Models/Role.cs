using BlogSolution.Types;
using System.Collections.Generic;

namespace Identity.Domain.Models
{
    public partial class Role : EntityBase, ISoftDeletable, IIdentifiable
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
