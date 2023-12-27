namespace GK.FSL.Auth.Models;

public record SignInDto
{
    public required string Login { get; set; }

    public required string Password { get; set; }

    public string? DeviceName { get; set; }

    public string? IpAddress { get; set; }
}