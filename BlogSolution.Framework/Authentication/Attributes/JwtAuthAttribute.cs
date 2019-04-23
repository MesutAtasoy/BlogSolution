using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BlogSolution.Framework.Authentication.Attributes
{
    public class JwtAuthAttribute : BlogSolutionAuthAttribute
    {
        public JwtAuthAttribute(string policy = "") : base(JwtBearerDefaults.AuthenticationScheme, policy)
        {
        }
    }
}
