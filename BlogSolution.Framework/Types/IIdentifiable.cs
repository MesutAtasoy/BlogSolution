using System;

namespace BlogSolution.Framework.Types
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
