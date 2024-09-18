using Bookify.Domain.Abstraction;

namespace Bookify.Domain.UnitTests.Abstraction;
public abstract class BaseTest
{
    public static T AssertDomainEventWasPublished<T>(Entity entity)
        where T : IDomainEvent
    {
        var domainEvent = entity.GetDomainEvents().OfType<T>().FirstOrDefault();

        if (domainEvent == null)
        {
            throw new Exception($"{typeof(T).Name} was not published.");
        }

        return domainEvent;
    }
}
