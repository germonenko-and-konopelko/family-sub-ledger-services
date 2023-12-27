using System.IdentityModel.Tokens.Jwt;
using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Models;

namespace GK.FSL.Auth.Services;

public class JwtAccessTokenGenerator : IAccessTokenGenerator
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public string GetToken(AccessTokenPayload tokenPayload)
    {
        throw new NotImplementedException();
    }
}