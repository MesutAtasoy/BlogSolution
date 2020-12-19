using BlogSolution.Types;
using System;

namespace Blog.Domain.Models
{
    public partial class PostTag : ISoftDeletable
    {
        public PostTag()
        {
            
        }
        public PostTag(Guid postId, Guid tagId)
        {
            PostId = postId;
            TagId = tagId;
        }

        public Guid PostId { get; set; }
        public Guid TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
        public bool IsDeleted { get; set; }
    }
}
