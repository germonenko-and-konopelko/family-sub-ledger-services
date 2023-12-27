namespace GK.FSL.Auth.Models;

public record AuthorizationResult
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public DateTimeOffset Expires { get; set; }
}