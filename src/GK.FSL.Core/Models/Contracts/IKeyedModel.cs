namespace GK.FSL.Core.Models.Contracts;

public interface IKeyedModel<TKey> where TKey : struct
{
    public TKey Id { get; set; }
}