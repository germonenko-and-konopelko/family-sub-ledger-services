using System.ComponentModel.DataAnnotations;
using GK.FSL.Core.Models.Contracts;

namespace GK.FSL.Core.Models;

public class Session : BaseModel<Guid>
{
    [Required, StringLength(200)]
    public string RefreshToken { get; set; } = string.Empty;

    [StringLength(100)]
    public string? ClientName { get; set; } = string.Empty;

    [StringLength(60)]
    public string? IpAddress { get; set; } = string.Empty;

    public DateTimeOffset LastRefresh { get; set; }

    public TimeSpan? IdleTimeoutOverride { get; set; }
}