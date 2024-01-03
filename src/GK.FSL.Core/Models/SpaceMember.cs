using System.ComponentModel.DataAnnotations;
using GK.FSL.Core.Models.Contracts;

namespace GK.FSL.Core.Models;

public class SpaceMember : ICreatedDateTrackingModel, IUpdatedDateTrackingModel
{
    public long SpaceId { get; set; }

    public long UserId { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }

    [Required]
    public List<SpacePermissions> Permissions { get; set; } = new();
}

public enum SpacePermissions
{
    Owner,
    Admin,
}