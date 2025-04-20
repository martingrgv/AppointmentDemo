namespace AppointmentDomain.Domain.Abstraction;

public interface IDomainEvent
{
    
    Guid EventId => Guid.NewGuid();
    DateTime OccuredOn { get; }
    string EventType => GetType().AssemblyQualifiedName!;
}