using AppointmentDomain.Domain.Abstraction;
using AppointmentDomain.Domain.ValueObjects;

namespace AppointmentDomain;

public class Appointer : Entity
{
    private List<WorkingHour> _workingHours = [];
    private List<Service> _services = [];
    
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public IReadOnlyCollection<WorkingHour> WorkingHours => _workingHours.AsReadOnly();
    public IReadOnlyCollection<Service> Services { get; }

    public bool IsWorkingAt(DateTime dateTime, TimeSpan duration)
    {
        var workingHour = WorkingHours.FirstOrDefault(h =>
            h.Day == dateTime.DayOfWeek);

        return workingHour?.Includes(dateTime.TimeOfDay, duration) ?? false;
    }

    public Service? GetService(Guid serviceId)
        => _services.FirstOrDefault(s => s.Id == serviceId);

    public void AddService(Guid serviceId, string name, TimeSpan duration, decimal price)
    {
        var service = Service.Create(
            serviceId,
            this.Id,
            name,
            duration,
            price);
        
        _services.Add(service);
    }

    public void UpdateService(Guid serviceId, string name, TimeSpan duration, decimal price)
    {
        var service = GetService(serviceId);

        if (service is null)
        {
            throw new InvalidOperationException("Service not found");
        }

        service.Update(
            name,
            duration,
            price);
    }

    public void RemoveService(Guid serviceId)
    {
        var service = GetService(serviceId);
        
        if (service is null)
        {
            throw new InvalidOperationException("Service not found");
        }

        _services.Remove(service);
    }
}   