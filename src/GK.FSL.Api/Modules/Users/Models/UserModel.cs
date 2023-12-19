namespace GK.FSL.Api.Modules.Users.Models;

public class UserModel
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public DateOnly SignUpDate { get; set; }
}