using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events;
public sealed record BookingCompletedDomainEvent(Guid BookingId) : IDomainEvent;
