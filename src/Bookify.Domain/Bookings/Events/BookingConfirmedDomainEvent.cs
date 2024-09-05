using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events;
public sealed record BookingConfirmedDomainEvent(Guid BookingId) : IDomainEvent;
