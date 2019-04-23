using BlogSolution.Framework.Types;
using System;
using System.Collections.Generic;

namespace Blog.Domain.Models
{
    public partial class Tag : EntityBase, ISoftDeletable, IIdentifiable
    {
        public Tag()
        {
            PostTags = new HashSet<PostTag>();
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
