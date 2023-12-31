using System.Security.Cryptography;
using GK.FSL.Common.Cryptography;
using GK.FSL.Common.Cryptography.Contracts;
using GK.FSL.Common.Validation.Contracts;
using GK.FSL.Core;
using GK.FSL.Core.Models;
using GK.FSL.Registration.Constants;
using GK.FSL.Registration.Contracts;
using GK.FSL.Registration.Models;
using GK.FSL.Registration.Options;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace GK.FSL.Registration.Services;

public class UserRegistrationService(
    CoreDbContext databaseContext,
    IFeatureManager featureManager,
    IHasher hasher,
    IOptionsSnapshot<RegistrationOptions> options,
    IValidationRunner<RegisterUserDto> validationRunner
) : IUserRegistrationService
{
    public async Task<User> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        await validationRunner.EnsureModelIsValidAsync(registerUserDto);

        var activateUser = await featureManager.IsEnabledAsync(FeatureFlags.AutomaticUserActivation);
        var user = new User
        {
            EmailAddress = registerUserDto.EmailAddress,
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            Status = activateUser ? UserStatus.Active : UserStatus.Inactive
        };

        var salt = RandomNumberGenerator.GetBytes(options.Value.RequiredPasswordSaltLength);
        var passwordHash = hasher.GetHash(registerUserDto.Password, salt, options.Value.RequiredPasswordByteLength);
        user.SetPassword(passwordHash, salt);

        databaseContext.Users.Add(user);
        await databaseContext.SaveChangesAsync();

        return user;
    }
}