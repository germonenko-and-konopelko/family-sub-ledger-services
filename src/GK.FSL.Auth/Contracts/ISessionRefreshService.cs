using GK.FSL.Auth.Models;

namespace GK.FSL.Auth.Contracts;

public interface ISessionRefreshService
{
    public Task<AuthorizationResult> RefreshSessionAsync(SessionRefreshDto sessionRefreshDto);
}