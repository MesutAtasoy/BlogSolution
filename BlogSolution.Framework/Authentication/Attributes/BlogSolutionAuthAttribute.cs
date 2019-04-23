using Microsoft.AspNetCore.Authorization;

namespace BlogSolution.Framework.Authentication.Attributes
{
    public class BlogSolutionAuthAttribute : AuthorizeAttribute
    {
        public BlogSolutionAuthAttribute(string scheme, string policy = "") : base(policy)
        {
            AuthenticationSchemes = scheme;
        }
    }
}
