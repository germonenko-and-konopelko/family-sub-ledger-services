using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GK.FSL.Core.Models.Configuration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(user => user.EmailAddress).IsUnique();

        builder.OwnsOne(user => user.Password, password =>
        {
            password.Property(p => p.Hash).HasColumnName("PasswordHash");
            password.Property(p => p.Salt).HasColumnName("PasswordSalt");
        });
    }
}