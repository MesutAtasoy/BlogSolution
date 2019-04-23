using System;
using System.Collections.Generic;

namespace Blog.Application.Dtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string FriendlyTitle { get; set; }
        public string PostContent { get; set; }
    }
}
