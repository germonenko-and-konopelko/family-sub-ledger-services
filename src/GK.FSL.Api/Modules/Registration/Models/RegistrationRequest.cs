namespace GK.FSL.Api.Modules.Registration.Models;

public sealed record RegistrationRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}