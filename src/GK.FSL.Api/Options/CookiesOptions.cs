using System.ComponentModel.DataAnnotations;

namespace GK.FSL.Api.Options;

public class CookiesOptions
{
    [Required]
    public CookiesOptionsEntry Sessions { get; set; } = null!;
}

public class CookiesOptionsEntry
{
    public bool HttpOnly { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [AllowedValues(SameSiteMode.None, SameSiteMode.Lax, SameSiteMode.Strict)]
    public SameSiteMode SameSite { get; set; }

    public bool Secure { get; set; }
}