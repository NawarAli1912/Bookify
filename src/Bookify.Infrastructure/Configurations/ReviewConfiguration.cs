using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;
internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Rating)
            .HasConversion(r => r.Value, value => Rating.Create(value).Value);

        builder.Property(r => r.Comment)
            .HasMaxLength(200)
            .HasConversion(comment => comment.Value, value => new Comment(value));

        builder.HasOne<Apartment>()
            .WithMany()
            .HasForeignKey(r => r.ApartmentId);

        builder.HasOne<Booking>()
            .WithMany()
            .HasForeignKey(r => r.BookingId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.UserId);
    }
}
