namespace GK.FSL.Api.Modules.Users.Models;

public record UserModel
{
    public string Id { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public DateOnly SignUpDate { get; set; }
}