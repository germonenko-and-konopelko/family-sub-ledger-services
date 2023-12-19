using GK.FSL.Core.Models;
using GK.FSL.Core.Models.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GK.FSL.Core;

public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}