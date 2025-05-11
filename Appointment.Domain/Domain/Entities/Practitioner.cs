using Appointment.Domain.Domain.Abstraction;
using Appointment.Domain.Domain.ValueObjects;

namespace Appointment.Domain.Domain.Entities;

public class Practitioner : Entity
{
    private readonly List<WorkingHour> _weeklySchedule = [];
    private readonly List<Guid> _serviceIds = [];

    private Practitioner(
        Guid id,
        string firstName,
        string lastName,
        Email email,
        PhoneNumber phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public Email Email { get; }
    public PhoneNumber PhoneNumber { get; }
    public IReadOnlyCollection<WorkingHour> WeeklySchedule => _weeklySchedule.AsReadOnly();
    public IReadOnlyCollection<Guid> Services => _serviceIds.AsReadOnly();

    public void SetWeeklySchedule(IEnumerable<WorkingHour> weeklySchedule)
    {
        _weeklySchedule.Clear();
        _weeklySchedule.AddRange(weeklySchedule);
    }

    public bool IsWorkingAt(DateTime dateTime, TimeSpan duration)
    {
        var workingHour = _weeklySchedule.FirstOrDefault(h =>
            h.Day == dateTime.DayOfWeek);

        return workingHour?.Includes(dateTime.TimeOfDay, duration) ?? false;
    }

    public void AddService(Guid serviceId)
    {
        if (!_serviceIds.Contains(serviceId))
        {
            _serviceIds.Add(serviceId);
        }
    }

    public void RemoveService(Guid serviceId)
    {
        _serviceIds.Remove(serviceId);
    }


    public static Practitioner Create(Guid id, string firstName, string lastName, Email email, PhoneNumber phoneNumber)
        => new(id, firstName, lastName, email, phoneNumber);
}
