using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Users.Events;
public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
