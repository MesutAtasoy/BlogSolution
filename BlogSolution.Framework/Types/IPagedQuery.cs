namespace BlogSolution.Framework.Types
{
    public interface IPagedQuery
    {
        int PageNumber { get; set; }

        int PageSize { get; set; }
    }
}
