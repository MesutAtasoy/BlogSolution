using BlogSolution.Types;
using System;
using System.Collections.Generic;

namespace Blog.Domain.Models
{
    public partial class Category : EntityBase, ISoftDeletable, IIdentifiable
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
