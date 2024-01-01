using System.Security.Cryptography;
using GK.FSL.Auth.Contracts;

namespace GK.FSL.Auth.Services;

public class RandomRefreshTokenGenerator : IRefreshTokenGenerator
{
    private const int TokenBytesLength = 128;

    public string GetToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(TokenBytesLength);
        return Convert.ToBase64String(randomBytes);
    }
}