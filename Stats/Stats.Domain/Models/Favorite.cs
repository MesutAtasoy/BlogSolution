using BlogSolution.Types;
using System;

namespace Stats.Domain.Models
{
    public class Favorite : IIdentifiable
    {
        public Favorite()
        {
                
        }

        public Favorite(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
        }
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
    }
}
