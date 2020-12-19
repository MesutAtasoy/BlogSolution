using BlogSolution.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Domain.Models
{
    public partial class Post : EntityBase, ISoftDeletable, IIdentifiable
    {
        public Post()
        {
            PostTags = new HashSet<PostTag>();
        }

        public Post(string title,
            string friendlyTitle,
            string postContent,
            Guid authorId,
            Guid categoryId)
        {
            Title = title;
            FriendlyTitle = friendlyTitle;
            PostContent = postContent;
            CategoryId = categoryId;
            AuthorId = authorId;
            IsActive = true;
            IsDeleted = false;
        }

        public Guid CategoryId { get; private set; }
        public Guid AuthorId { get; private set; }
        public string Title { get; private set; }
        public string FriendlyTitle { get; private set; }
        public string PostContent { get; private set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }


        public void AddPostTag(PostTag postTag)
        {
            if (PostTags != null && PostTags.Any())
            {
                PostTags.Add(postTag);
            }
            else
            {
                PostTags = new List<PostTag> {postTag};
            }
        }

        public void Update(string title,
            string friendlyTitle,
            string postContent,
            Guid categoryId,
            Guid userId)
        {
            CategoryId = categoryId;
            PostContent = postContent;
            Title = title;
            UpdatedBy = userId;
            UpdatedDate = DateTime.UtcNow;
            FriendlyTitle = friendlyTitle;
        }

        public void MarkAsDelete(Guid userId)
        {
            IsDeleted = true;
            UpdatedBy = userId;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}