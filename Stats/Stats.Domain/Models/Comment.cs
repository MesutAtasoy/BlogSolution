using BlogSolution.Types;
using System;

namespace Stats.Domain.Models
{
    public class Comment : IIdentifiable
    {
        public Comment()
        {
                
        }

        public Comment(Guid userId, string commentText)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CommentText = commentText;
            CreateDate = DateTime.Now;
        }
        
        public Guid Id { get; private set; }
        public Guid UserId { get;  private set; }
        public string CommentText{ get;  private set; }
        public DateTime CreateDate { get;  private set; }
    }
}
