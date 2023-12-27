using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GK.FSL.Core.Models.Configuration;

public class SessionEntityTypeConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("session", "auth");
        builder.HasIndex(s => s.RefreshToken).IsUnique();
        builder.HasIndex(s => s.LastRefresh);
        builder.HasIndex(s => new { s.LastRefresh, s.IdleTimeoutOverride });
    }
}