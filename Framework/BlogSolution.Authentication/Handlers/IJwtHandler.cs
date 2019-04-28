using BlogSolution.Authentication.Models;
using System.Collections.Generic;

namespace BlogSolution.Authentication.Handlers
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, string role = null, IDictionary<string, string> claims = null);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}
