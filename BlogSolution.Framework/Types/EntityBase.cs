using System;
using System.ComponentModel.DataAnnotations;

namespace BlogSolution.Framework.Types
{
    public partial class EntityBase
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}
