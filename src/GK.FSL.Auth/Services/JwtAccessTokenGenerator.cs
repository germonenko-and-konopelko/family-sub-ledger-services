using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Models;
using GK.FSL.Auth.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace GK.FSL.Auth.Services;

public class JwtAccessTokenGenerator(IOptions<AccessTokenOptions> options) : IAccessTokenGenerator
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public string GetToken(AccessTokenPayload tokenPayload)
    {
        var securityKey = new SymmetricSecurityKey(options.Value.SigningKey);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtHeader = new JwtHeader(signingCredentials);
        var claims = new List<Claim>();

        if (tokenPayload.SessionId is not null)
        {
            claims.Add(new (ClaimNames.Sid, tokenPayload.SessionId));
        }

        if (tokenPayload.UserId is not null)
        {
            claims.Add(new (ClaimNames.Sub, tokenPayload.UserId));
        }

        if (tokenPayload.EmailAddress is not null)
        {
            claims.Add(new (ClaimNames.Email, tokenPayload.EmailAddress));
        }

        if (tokenPayload.FirstName is not null)
        {
            claims.Add(new (ClaimNames.GivenName, tokenPayload.FirstName));
        }

        if (tokenPayload.LastName is not null)
        {
            claims.Add(new (ClaimNames.FamilyName, tokenPayload.LastName));
        }

        var issuedAt = DateTime.UtcNow;
        var jwtPayload = new JwtPayload(
            issuer:  options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            notBefore: null,
            expires: issuedAt.Add(options.Value.LifetimeSpan),
            issuedAt: issuedAt
        );

        var jwtToken = new JwtSecurityToken(jwtHeader, jwtPayload);
        return _jwtSecurityTokenHandler.WriteToken(jwtToken);
    }
}