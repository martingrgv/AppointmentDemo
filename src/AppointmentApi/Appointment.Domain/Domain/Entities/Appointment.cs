using Appointment.Domain.Domain.Abstraction;
using Appointment.Domain.Domain.Enums;
using Appointment.Domain.Domain.ValueObjects;

namespace Appointment.Domain.Domain.Entities;

public class Appointment : AggregateRoot
{
    private Appointment(Guid id, Guid practitionerId, Guid customerId, Guid serviceId, DateTime startTime)
    {
        Id = id;
        PractitionerId = practitionerId;
        CustomerId = customerId;
        ServiceId = serviceId;
        StartTime = startTime;
    }

    public Guid CustomerId { get; }
    public Guid PractitionerId { get; }
    public Guid ServiceId { get; }
    public DateTime StartTime { get; }
    public AppointmentStatus Status { get; private set; }

    public void Confirm()
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            throw new InvalidOperationException();
        }

        Status = AppointmentStatus.Confirmed;
        // Call event
    }

    public void Cancel(string reason)
    {
        if (Status == AppointmentStatus.Completed || Status == AppointmentStatus.Canceled)
        {
            throw new InvalidOperationException();
        }

        Status = AppointmentStatus.Canceled;
        // Call event
    }

    public static Appointment Schedule(
        Guid id,
        Guid practitionerId,
        Guid customerId,
        Guid serviceId,
        DateTime startTime)
    {
        if (serviceId == Guid.Empty)
        {
            throw new InvalidOperationException();
        }

        if (practitionerId == Guid.Empty)
        {
            throw new InvalidOperationException();
        }

        if (customerId == Guid.Empty)
        {
            throw new InvalidOperationException();
        }

        return new (id, customerId, practitionerId, serviceId, startTime)
        {
            Status = AppointmentStatus.Scheduled
        };
    }
}
