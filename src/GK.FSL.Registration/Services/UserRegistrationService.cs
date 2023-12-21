using GK.FSL.Core;
using GK.FSL.Registration.Contracts;
using GK.FSL.Registration.Models;

namespace GK.FSL.Registration.Services;

public class UserRegistrationService(CoreDbContext databaseContext) : IUserRegistrationService
{
    public Task RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        throw new NotImplementedException();
    }
}