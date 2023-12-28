namespace GK.FSL.Auth.Contracts;

public interface IRefreshTokenGenerator
{
    public string GetToken();
}