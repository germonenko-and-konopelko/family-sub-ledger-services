namespace GK.FSL.Core.Models.Contracts;

public interface IUpdatedDateTrackingModel
{
    public DateTimeOffset Updated { get; set; }
}