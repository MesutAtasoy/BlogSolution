using System;

namespace Stats.Application.Models
{
    public class CommentRequestModel
    {
        public Guid PostId { get; set; }
        public string Comment { get; set; }
    }
}
