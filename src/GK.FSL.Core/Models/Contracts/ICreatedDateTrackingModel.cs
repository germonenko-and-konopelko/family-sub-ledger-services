namespace GK.FSL.Core.Models.Contracts;

public interface ICreatedDateTrackingModel
{
    public DateTimeOffset Created { get; set; }
}