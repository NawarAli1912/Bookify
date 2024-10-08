﻿using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;
internal sealed class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasMaxLength(200)
            .HasConversion(fn => fn.Value, value => new FirstName(value));

        builder.Property(u => u.LastName)
            .HasMaxLength(200)
            .HasConversion(ln => ln.Value, value => new LastName(value));

        builder.Property(u => u.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => new Domain.Users.Email(value));

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasIndex(u => u.IdentityId).IsUnique();
    }
}
