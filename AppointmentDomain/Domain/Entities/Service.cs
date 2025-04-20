using AppointmentDomain.Domain.Abstraction;

namespace AppointmentDomain;

public class Service : Entity
{
    private Service(Guid id, Guid appointerId, string name, TimeSpan duration, decimal price)
    {
        Id = id;
        AppointerId = appointerId;
        Name = name;
        Price = price;
    }

    public Guid AppointerId { get; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    public void Update(string name, TimeSpan duration, decimal price)
    {
        Name = name;
        Price = price;
    }

    public static Service Create(Guid id, Guid appointerId, string name, TimeSpan duration, decimal price)
        => new Service(id, appointerId, name, duration, price);
}