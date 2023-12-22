namespace GK.FSL.Registration.Models;

public record RegisterUserDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string EmailAddress { get; set; }

    public required string Password { get; set; }
}