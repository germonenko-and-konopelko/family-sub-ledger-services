namespace GK.FSL.Auth.Models;

public record AccessTokenPayload
{
    public string? UserId { get; set; }

    public string? SessionId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }
}