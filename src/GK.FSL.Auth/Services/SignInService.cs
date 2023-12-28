using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Models;
using GK.FSL.Core;
using Microsoft.EntityFrameworkCore;

namespace GK.FSL.Auth.Services;

public class SignInService(
    CoreDbContext databaseContext
) : ISignInService
{
    public async Task<AuthorizationResult> SignInAsync(SignInDto signInDto)
    {
        var user = await databaseContext.Users.FirstAsync(u => u.EmailAddress == signInDto.Login);

        throw new NotImplementedException();
    }
}