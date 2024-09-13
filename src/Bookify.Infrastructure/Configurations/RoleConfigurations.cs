using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;
internal sealed class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(r => r.Id);

        builder.HasMany(r => r.Users)
            .WithMany(r => r.Roles);

        builder.HasMany(r => r.Permissions)
            .WithMany();

        builder.HasData(Role.Registered);

        builder.HasData(Role.Admin);
    }
}
