using System;
using System.Collections.Generic;
using BlogSolution.Types;

namespace Stats.Domain.Models
{
    public class BlogStatsItem : IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public int FavouriteCount { get; set; }
        public int CommentCount { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Favorite> Favorites { get; set; }

        public BlogStatsItem()
        {
            this.Comments = new List<Comment>();
            this.Favorites = new List<Favorite>();
        }
    }
}
