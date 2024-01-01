using System.Linq.Expressions;
using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Options;
using GK.FSL.Core;
using GK.FSL.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GK.FSL.Auth.Services;

public class StaleSessionCleanupService(CoreDbContext databaseContext, IOptionsSnapshot<SessionOptions> options)
    : IStaleSessionCleanupService
{
    private const int BatchSize = 1000;

    public async Task RunCleanupAsync(CancellationToken stoppingToken)
    {
        var now = DateTimeOffset.UtcNow;
        var idleThreshold = now - options.Value.IdleTimespan;

        Expression<Func<Session, bool>> filterPredicate =
            s => s.LastRefresh < idleThreshold || s.LastRefresh - s.IdleTimeoutOverride < now;

        var sessionsToCleanupCount = await databaseContext.Sessions.LongCountAsync(filterPredicate, stoppingToken);

        for (var offset = 0; offset < sessionsToCleanupCount; offset += BatchSize)
        {
            await databaseContext.Sessions
                .Where(filterPredicate)
                .OrderBy(s => s.Created)
                .Take(BatchSize)
                .ExecuteDeleteAsync(stoppingToken);
        }
    }
}