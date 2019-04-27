namespace BlogSolution.Types
{
    public abstract class PagedQuery : IPagedQuery
    {
        public int PageNumber  { get; set; } 

        public int PageSize { get; set; }
    }
}
