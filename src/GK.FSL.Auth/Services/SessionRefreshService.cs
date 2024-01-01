using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Models;
using GK.FSL.Auth.Options;
using GK.FSL.Auth.Resources;
using GK.FSL.Common.Cryptography.Contracts;
using GK.FSL.Core;
using GK.FSL.Core.Exceptions;
using GK.FSL.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace GK.FSL.Auth.Services;

public class SessionRefreshService(
    IAccessTokenGenerator accessTokenGenerator,
    CoreDbContext databaseContext,
    IEncoder<long> encoder,
    IStringLocalizer<ErrorMessages> errorMessages,
    IOptionsSnapshot<SessionOptions> options,
    IRefreshTokenGenerator refreshTokenGenerator
) : ISessionRefreshService
{
    public async Task<AuthorizationResult> RefreshSessionAsync(SessionRefreshDto sessionRefreshDto)
    {
        var session = await databaseContext.Sessions
            .FirstOrDefaultAsync(s => s.RefreshToken == sessionRefreshDto.RefreshToken);

        if (session is null)
        {
            throw new CoreException(
                errorMessages[nameof(ErrorMessages.SessionExpired)],
                errorMessages[nameof(ErrorMessages.SessionExpiredDescription)]
            );
        }

        User? user = null;
        if (session.UserId is not null)
        {
            user = await databaseContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == session.UserId);

            if (user?.Status != UserStatus.Active)
            {
                throw new CoreException(
                    errorMessages[nameof(ErrorMessages.SessionUserNotFound)],
                    errorMessages[nameof(ErrorMessages.SessionUserNotFoundDescription)]
                );
            }
        }

        var refreshDate = DateTimeOffset.UtcNow;
        var refreshToken = new Token(
            refreshTokenGenerator.GetToken(),
            refreshDate.Add(options.Value.IdleTimespan)
        );

        session.RefreshToken = refreshToken.Value;
        session.LastRefresh = refreshDate;

        var accessTokenPayload = new AccessTokenPayload
        {
            SessionId = encoder.Encode(session.Id),
            UserId = user is null ? null : encoder.Encode(user.Id),
            FirstName = user?.FirstName,
            LastName = user?.LastName,
            EmailAddress = user?.EmailAddress,
        };
        var accessToken = accessTokenGenerator.GetToken(accessTokenPayload);

        return new AuthorizationResult(accessToken, refreshToken);
    }
}