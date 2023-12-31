namespace GK.FSL.Auth.Models;

public record SessionRefreshDto
{
    public required string RefreshToken { get; set; }
}