using System;

namespace BlogSolution.Types
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
