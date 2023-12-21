using GK.FSL.Core.Models;
using GK.FSL.Core.Models.Configuration;
using GK.FSL.Core.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GK.FSL.Core;

public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Session> Sessions => Set<Session>();

    public override int SaveChanges()
    {
        SetCreatedAndUpdatedDate();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetCreatedAndUpdatedDate();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        SetCreatedAndUpdatedDate();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetCreatedAndUpdatedDate();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("core");

        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }

    private void SetCreatedAndUpdatedDate()
    {
        var now = DateTimeOffset.Now;
        SetCreatedDate(now);
        SetUpdatedDate(now);
    }

    private void SetCreatedDate(DateTimeOffset createdDate)
    {
        var updatedEntries = ChangeTracker
            .Entries<ICreatedDateTrackingModel>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in updatedEntries)
        {
            entry.Entity.Created = createdDate;
        }
    }

    private void SetUpdatedDate(DateTimeOffset updatedDate)
    {
        var updatedEntries = ChangeTracker
            .Entries<IUpdatedDateTrackingModel>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in updatedEntries)
        {
            entry.Entity.Updated = updatedDate;
        }
    }
}