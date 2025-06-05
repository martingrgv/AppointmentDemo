namespace Appointment.Api.Dtos;

public class ServiceDto
{
    public string Name { get; set; } = null!;
    public TimeSpan Duration { get; set; }
    public decimal Price { get; set; }
}