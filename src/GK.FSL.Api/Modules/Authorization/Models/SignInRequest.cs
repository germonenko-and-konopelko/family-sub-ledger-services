namespace GK.FSL.Api.Modules.Authorization.Models;

public record SignInRequest
{
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? DeviceName { get; set; }
}