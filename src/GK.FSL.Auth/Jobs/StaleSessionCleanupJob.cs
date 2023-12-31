using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GK.FSL.Auth.Jobs;

public class StaleSessionCleanupJob(
    ILogger<StaleSessionCleanupJob> logger,
    IServiceProvider serviceProvider
) : BackgroundService
{
    private static readonly EventId CleanupFailedEventId = new(1000, "Session Cleanup Failed");
    private static readonly EventId CleanupCompletedEventId = new(1001, "Session Cleanup Failed");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IStaleSessionCleanupService>();

                await service.RunCleanupAsync(stoppingToken);
                logger.LogInformation(CleanupCompletedEventId, "Session cleanup completed");

                var options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<SessionOptions>>();
                await Task.Delay(options.Value.StaleSessionCleanupInterval, stoppingToken);
            }
            catch (Exception e)
            {
                logger.LogError(CleanupFailedEventId, e, "Error occured trying to cleanup stale sessions");
            }
        }
    }
}