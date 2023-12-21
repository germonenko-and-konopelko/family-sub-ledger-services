using GK.FSL.Registration.Models;

namespace GK.FSL.Registration.Contracts;

public interface IUserRegistrationService
{
    public Task RegisterUserAsync(RegisterUserDto registerUserDto);
}