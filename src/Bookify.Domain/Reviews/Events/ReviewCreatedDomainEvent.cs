using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Reviews.Events;
public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;
