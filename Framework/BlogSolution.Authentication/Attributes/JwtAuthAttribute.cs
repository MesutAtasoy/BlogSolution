using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BlogSolution.Authentication.Attributes
{
    public class JwtAuthAttribute : BlogSolutionAuthAttribute
    {
        public JwtAuthAttribute(string policy = "") : base(JwtBearerDefaults.AuthenticationScheme, policy)
        {
        }
    }
}
