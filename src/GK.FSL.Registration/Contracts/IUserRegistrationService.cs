using GK.FSL.Core.Models;
using GK.FSL.Registration.Models;

namespace GK.FSL.Registration.Contracts;

public interface IUserRegistrationService
{
    public Task<User> RegisterUserAsync(RegisterUserDto registerUserDto);
}