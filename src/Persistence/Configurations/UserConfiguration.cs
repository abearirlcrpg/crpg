using Crpg.Domain.Entities.Users;
using Crpg.Domain.Entities.Captains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crpg.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => new { u.Platform, u.PlatformUserId }).IsUnique();

        builder
            .HasOne(u => u.ActiveCharacter)
            .WithOne()
            .HasForeignKey<User>(u => u.ActiveCharacterId);

        builder.HasOne(u => u.Captain)
            .WithOne(c => c.User)
            .HasForeignKey<Captain>(c => c.UserId);

        builder.HasQueryFilter(u => u.DeletedAt == null);

        builder.Property(u => u.Version).IsRowVersion();
    }
}
