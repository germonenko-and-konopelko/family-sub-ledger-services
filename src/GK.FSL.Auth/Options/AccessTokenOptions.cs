using System.ComponentModel.DataAnnotations;

namespace GK.FSL.Auth.Options;

public class AccessTokenOptions
{
    [Required]
    public string Audience {get; set;} = string.Empty;

    [Required]
    public string Issuer { get; set; } = string.Empty;

    public TimeSpan LifetimeSpan { get; set; } = TimeSpan.FromMinutes(1);

    [Required, MinLength(32)]
    public byte[] SigningKey { get; set; } = [];
}
