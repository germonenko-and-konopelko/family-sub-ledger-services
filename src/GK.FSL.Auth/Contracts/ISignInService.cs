using GK.FSL.Auth.Models;

namespace GK.FSL.Auth.Contracts;

public interface ISignInService
{
    public Task<AuthorizationResult> SignInAsync(SignInDto signInDto);
}