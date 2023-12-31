namespace GK.FSL.Api.Modules.Authorization.Models;

public record SignInRequest
{
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? Device { get; set; }

    public static SignInRequest GetSwaggerExample() => new()
    {
        Login = "emilia.müller@gmail.com",
        Password = "1234",
        Device = "swagger",
    };
}