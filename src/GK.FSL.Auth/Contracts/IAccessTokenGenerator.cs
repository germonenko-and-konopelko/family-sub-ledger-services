using GK.FSL.Auth.Models;

namespace GK.FSL.Auth.Contracts;

public interface IAccessTokenGenerator
{
    public Token GetToken(AccessTokenPayload tokenPayload);
}