using System.ComponentModel.DataAnnotations;
using GK.FSL.Core.Models.Contracts;

namespace GK.FSL.Core.Models;

public class Space : BaseModel<long>
{
    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public SpaceStatus Status { get; set; } = SpaceStatus.None;
}

public enum SpaceStatus
{
    None = 0,
    Active = 10,
}