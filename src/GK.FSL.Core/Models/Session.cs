using System.ComponentModel.DataAnnotations;
using GK.FSL.Core.Models.Contracts;

namespace GK.FSL.Core.Models;

public class Session : BaseModel<long>
{
    public long? UserId { get; set; }

    [Required, StringLength(200)]
    public string RefreshToken { get; set; } = string.Empty;

    [StringLength(100)]
    public string? ClientName { get; set; } = string.Empty;

    [StringLength(60)]
    public string? IpAddress { get; set; } = string.Empty;

    public DateTimeOffset LastRefresh { get; set; }

    public TimeSpan? IdleTimeoutOverride { get; set; }
}