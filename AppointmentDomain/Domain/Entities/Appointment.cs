using AppointmentDomain.Domain.Abstraction;

namespace AppointmentDomain;

public class Appointment : AggregateRoot
{
    public Guid CustomerId { get; }
    public Guid AppointerId { get; }
    public Guid ServiceId { get; }
    public DateTime StartTime { get; }
    public TimeSpan Duration { get; }

    public static Appointment Scheudle(
        Guid id,
        Guid customerId,
        Guid appointerId,
        Guid serviceId,
        DateTime startTime)
    {

    }

    public DateTime EndTime => StartTime + Duration;
}