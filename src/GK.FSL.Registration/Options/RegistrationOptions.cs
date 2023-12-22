using System.ComponentModel.DataAnnotations;

namespace GK.FSL.Registration.Options;

public record RegistrationOptions
{
    [Required, Range(8, 200)]
    public int RequiredPasswordByteLength { get; set; } = 64;

    [Required, Range(8, 200)]
    public int RequiredPasswordSaltLength { get; set; } = 32;
}