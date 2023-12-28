using GK.FSL.Auth.Models;

namespace GK.FSL.Auth.Contracts;

public interface IAccessTokenGenerator
{
    public string GetToken(AccessTokenPayload tokenPayload);
}