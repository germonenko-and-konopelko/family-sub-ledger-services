using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GK.FSL.Core.Models.Configuration;

public class SpaceMemberEntityTypeConfiguration : IEntityTypeConfiguration<SpaceMember>
{
    public void Configure(EntityTypeBuilder<SpaceMember> builder)
    {
        builder.HasKey(sm => new { sm.SpaceId, sm.UserId });

        builder.Property(sm => sm.Permissions)
            .HasColumnType("array[]::varchar[]")
            .HasConversion<List<string>>()
            .HasDefaultValueSql("array[]::varchar[]");
    }
}