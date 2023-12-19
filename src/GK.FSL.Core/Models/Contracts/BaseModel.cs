namespace GK.FSL.Core.Models.Contracts;

public abstract class BaseModel<TKey> : IKeyedModel<TKey>, ICreatedDateTrackingModel, IUpdatedDateTrackingModel
    where  TKey : struct
{
    public TKey Id { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }
}