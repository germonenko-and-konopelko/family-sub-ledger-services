using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Models;
using GK.FSL.Auth.Resources;
using GK.FSL.Common.Cryptography.Contracts;
using GK.FSL.Core;
using GK.FSL.Core.Exceptions;
using GK.FSL.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GK.FSL.Auth.Services;

public class SignInService(
    IAccessTokenGenerator accessTokenGenerator,
    CoreDbContext databaseContext,
    IEncoder<long> encoder,
    IStringLocalizer<ErrorMessages> errorMessages,
    IHasher hasher,
    IRefreshTokenGenerator refreshTokenGenerator
) : ISignInService
{
    private CoreException InvalidCredentialsException => new(
        errorMessages[nameof(ErrorMessages.InvalidCredentials)],
        errorMessages[nameof(ErrorMessages.InvalidCredentialsDescription)]
    );

    public async Task<AuthorizationResult> SignInAsync(SignInDto signInDto)
    {
        var user = await databaseContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == signInDto.Login);

        if (user?.Password is null)
        {
            throw InvalidCredentialsException;
        }

        var providedPassword = hasher.GetHash(signInDto.Password, user.Password.Salt, user.Password.Hash.Length);
        var hashesMatch = providedPassword.SequenceEqual(user.Password.Hash);

        if (!hashesMatch)
        {
            throw InvalidCredentialsException;
        }

        var refreshToken = refreshTokenGenerator.GetToken();
        var session = new Session
        {
            LastRefresh = DateTimeOffset.UtcNow,
            IpAddress = signInDto.IpAddress,
            ClientName = signInDto.DeviceName,
            RefreshToken = refreshToken,
        };

        databaseContext.Sessions.Add(session);
        await databaseContext.SaveChangesAsync();

        var payload = new AccessTokenPayload
        {
            UserId = encoder.Encode(user.Id),
            SessionId = encoder.Encode(session.Id),
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress,
        };

        throw new NotImplementedException();
    }
}