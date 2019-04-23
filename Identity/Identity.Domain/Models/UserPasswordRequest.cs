using BlogSolution.Framework.Types;
using System;

namespace Identity.Domain.Models
{
    public partial class UserPasswordRequest : EntityBase, ISoftDeletable, IIdentifiable
    {
        public Guid UserId { get; set; }
        public Guid ActivationCode { get; set; }
        public DateTime? UsedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual User User { get; set; }
    }
}
