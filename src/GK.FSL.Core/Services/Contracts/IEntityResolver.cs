using GK.FSL.Core.Models.Contracts;

namespace GK.FSL.Core.Services.Contracts;

public interface IEntityResolver
{
    public Task<bool> ExistsAsync<TModel>(long key) where TModel : class, IKeyedModel<long>;
}