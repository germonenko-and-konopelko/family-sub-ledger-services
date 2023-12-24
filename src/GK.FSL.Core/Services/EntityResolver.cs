using GK.FSL.Core.Models.Contracts;
using GK.FSL.Core.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GK.FSL.Core.Services;

public class EntityResolver(CoreDbContext databaseContext) : IEntityResolver
{
    public Task<bool> ExistsAsync<TModel>(long key) where TModel : class, IKeyedModel<long>
    {
        return databaseContext.Set<TModel>().AnyAsync(entity => entity.Id == key);
    }
}