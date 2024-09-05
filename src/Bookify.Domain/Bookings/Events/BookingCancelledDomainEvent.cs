using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events;
public sealed record BookingCancelledDomainEvent(Guid BookingId) : IDomainEvent;
