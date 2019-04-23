using BlogSolution.Framework.Authentication.Models;
using System.Collections.Generic;

namespace BlogSolution.Framework.Authentication.Handlers
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, string role = null, IDictionary<string, string> claims = null);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}
