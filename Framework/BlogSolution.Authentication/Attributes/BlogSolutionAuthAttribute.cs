using Microsoft.AspNetCore.Authorization;

namespace BlogSolution.Authentication.Attributes
{
    public class BlogSolutionAuthAttribute : AuthorizeAttribute
    {
        public BlogSolutionAuthAttribute(string scheme, string policy = "") : base(policy)
        {
            AuthenticationSchemes = scheme;
        }
    }
}
