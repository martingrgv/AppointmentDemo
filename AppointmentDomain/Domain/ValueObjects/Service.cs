namespace AppointmentDomain.Domain.ValueObjects;

public record class Service
{
    private Service(string name, TimeSpan duration, decimal price)
    {
        Name = name;
        Duration = duration;
        Price = price;
    }

    public string Name { get; }
    public TimeSpan Duration { get; }
    public decimal Price { get; }

    public static Service Create(string name, TimeSpan duration, decimal price)
        => new(name, duration, price);
}
