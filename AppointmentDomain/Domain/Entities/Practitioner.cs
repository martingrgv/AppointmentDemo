using AppointmentDomain.Domain.Abstraction;
using AppointmentDomain.Domain.ValueObjects;

namespace AppointmentDomain.Domain.Entities;

public class Practitioner : Entity
{
    private readonly List<WorkingHour> _weeklySchedule = [];
    private readonly List<Service> _services = [];

    private Practitioner(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public IReadOnlyCollection<WorkingHour> WeeklySchedule => _weeklySchedule.AsReadOnly();
    public IReadOnlyCollection<Service> Services => _services.AsReadOnly();

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

    public void AddService(Service service)
    {
        if (!_services.Contains(service))
        {
            _services.Add(service);
        }
    }

    public void RemoveService(Service service)
    {
        _services.Remove(service);
    }


    public static Practitioner Create(Guid id, string firstName, string lastName, string email, string phoneNumber)
        => new(id, firstName, lastName, email, phoneNumber);
}
