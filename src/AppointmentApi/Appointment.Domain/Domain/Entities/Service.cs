using Appointment.Domain.Domain.Abstraction;

namespace Appointment.Domain.Domain.Entities;

public class Service : Entity
{
    private Service(string name, TimeSpan duration, decimal price)
    {
        Name = name;
        Duration = duration;
        Price = price;
    }

    public string Name { get; private set; }
    public TimeSpan Duration { get; private set; }
    public decimal Price { get; private set; }

    public void Update(string name, TimeSpan duration, decimal price)
    {
        Name = name;
        Duration = duration;
        Price = price;
    }

    public static Service Create(string name, TimeSpan duration, decimal price)
        => new(name, duration, price);
}
