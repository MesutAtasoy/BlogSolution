using BlogSolution.Framework.Types;
using System;
using System.Collections.Generic;

namespace Blog.Domain.Models
{
    public partial class Post : EntityBase, ISoftDeletable, IIdentifiable
    {
        public Post()
        {
            PostTags = new HashSet<PostTag>();
        }
        
        public Guid CategoryId { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string FriendlyTitle { get; set; }
        public string PostContent { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
