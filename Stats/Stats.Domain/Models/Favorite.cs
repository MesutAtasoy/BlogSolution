using BlogSolution.Framework.Types;
using System;

namespace Stats.Domain.Models
{
    public class Favorite : IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
