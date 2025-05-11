namespace Appointment.Domain.Domain.Abstraction;

public class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents;
    
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}