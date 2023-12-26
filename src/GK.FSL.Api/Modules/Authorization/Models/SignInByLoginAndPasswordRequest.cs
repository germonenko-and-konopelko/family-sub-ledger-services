namespace GK.FSL.Api.Modules.Authorization.Models;

public record SignInByLoginAndPasswordRequest
{
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}