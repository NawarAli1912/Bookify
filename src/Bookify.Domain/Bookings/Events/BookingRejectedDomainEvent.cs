using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events;
public sealed record BookingRejectedDomainEvent(Guid BookingId) : IDomainEvent;
