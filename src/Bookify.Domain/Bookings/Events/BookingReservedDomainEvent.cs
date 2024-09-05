using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events;
public sealed record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent;
