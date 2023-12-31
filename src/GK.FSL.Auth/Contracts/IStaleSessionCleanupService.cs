namespace GK.FSL.Auth.Contracts;

public interface IStaleSessionCleanupService
{
    public Task RunCleanupAsync(CancellationToken stoppingToken);
}