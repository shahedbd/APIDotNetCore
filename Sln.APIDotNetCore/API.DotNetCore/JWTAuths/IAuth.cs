namespace API.DotNetCore.JWTAuths
{
    public interface IAuth
    {
        string GenerateJSONWebToken(int ExpireIn);
        string GenerateRefreshToken();
    }
}
