namespace AppointmentDomain.Domain.Abstraction;

public class AggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents;
    
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}