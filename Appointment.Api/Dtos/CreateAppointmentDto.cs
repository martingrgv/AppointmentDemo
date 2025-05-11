namespace Appointment.Api.Dtos;

public class CreateAppointmentDto
{
    public Guid CustomerId { get; set; }
    public Guid PractitionerId { get; set; }
    public Guid ServiceId { get; set; }
}