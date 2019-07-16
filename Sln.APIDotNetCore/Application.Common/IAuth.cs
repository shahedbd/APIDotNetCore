namespace Application.Common
{
    public interface IAuth
    {
        string GenerateJSONWebToken(int ExpireIn);
        string GenerateRefreshToken();
    }
}
