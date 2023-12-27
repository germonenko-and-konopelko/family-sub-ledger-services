using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Models;
using GK.FSL.Auth.Resources;
using GK.FSL.Common.Cryptography.Contracts;
using GK.FSL.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GK.FSL.Auth.Services;

public class SignInService(
    CoreDbContext databaseContext,
    IEncoder<long> encoder,
    IStringLocalizer<ErrorMessages> errorMessages
) : ISignInService
{
    public async Task<AuthorizationResult> SignInAsync(SignInDto signInDto)
    {
        var user = await databaseContext.Users.FirstAsync(u => u.EmailAddress == signInDto.Login);

        throw new NotImplementedException();
    }
}