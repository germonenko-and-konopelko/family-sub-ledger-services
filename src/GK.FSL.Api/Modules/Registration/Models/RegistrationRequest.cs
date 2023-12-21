using System.ComponentModel.DataAnnotations;

namespace GK.FSL.Api.Modules.Registration.Models;

public sealed record RegistrationRequest
{
    [Required, StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(250), EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Password { get; set; } = string.Empty;
}