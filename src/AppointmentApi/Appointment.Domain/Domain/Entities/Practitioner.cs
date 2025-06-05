using Appointment.Domain.Domain.Abstraction;
using Appointment.Domain.Domain.ValueObjects;

namespace Appointment.Domain.Domain.Entities;

public class Practitioner : Entity
{
    private readonly List<WorkingHour> _weeklySchedule = [];
    private readonly List<Service> _services = [];

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

    public void AssignService(string name, decimal price, TimeSpan duration)
    {
        if (_services.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"A service with offering {name} already exists!");

        var service = Service.Create(name, duration, price);
        _services.Add(service);
    }

    public void UpdateService(Guid serviceId, string name, decimal price, TimeSpan duration)
    {
        var service = _services.FirstOrDefault(s => s.Id == serviceId);

        if (service is not null)
            service.Update(name, duration, price);
        else
            throw new InvalidOperationException($"Service with ID: {serviceId} was not found!");
    }

    public void RemoveService(Guid serviceId)
    {
        var service = _services.FirstOrDefault(s => s.Id == serviceId);

        if (service is not null)
            _services.Remove(service);
        else
            throw new InvalidOperationException($"Service with ID: {serviceId} was not found!");
    }


    public static Practitioner Create(Guid id, string firstName, string lastName, Email email, PhoneNumber phoneNumber)
        => new(id, firstName, lastName, email, phoneNumber);
}
