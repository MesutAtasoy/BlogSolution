using System;
using System.Collections.Generic;
using System.Linq;
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

        public BlogStatsItem(Guid postId)
        {
            PostId = postId;
            this.Comments = new List<Comment>();
            this.Favorites = new List<Favorite>();
        }

        public void AddComment(string comment, Guid userId)
        {
            var newComment = new Comment(userId, comment);
            if (Comments != null && Comments.Any())
            {
                Comments.Add(newComment);
            }
            else
            {
                Comments = new List<Comment> {newComment};
            }

            CommentCount += 1;
        }

        public void AddFavorite(Guid userId)
        {
            var favorite = new Favorite(userId);
            if (Favorites != null && Favorites.Any())
            {
                Favorites.Add(favorite);
            }
            else
            {
                Favorites = new List<Favorite> {favorite};
            }

            FavouriteCount += 1;
        }
    }
}