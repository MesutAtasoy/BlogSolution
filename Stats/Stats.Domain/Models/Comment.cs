using BlogSolution.Framework.Types;
using System;

namespace Stats.Domain.Models
{
    public class Comment : IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CommentText{ get; set; }
        public DateTime CreateDate { get; set; }
    }
}
