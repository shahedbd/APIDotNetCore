using System.Security.Claims;

namespace API.DotNetCore.JWTAuths
{
    public interface IAuth
    {
        string GenerateJSONWebToken(int ExpireIn);
        string GenerateRefreshToken();
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
