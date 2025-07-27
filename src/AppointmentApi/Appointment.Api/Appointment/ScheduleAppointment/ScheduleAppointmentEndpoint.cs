using Entities = Appointment.Domain.Domain.Entities;
using Appointment.Infrastructure.Data;
using Carter;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Api.Appointment.ScheduleAppointment;

public record ScheduleAppointmentRequest(Guid PractitionerId, Guid CustomerId, Guid ServiceId, DateTime StartTime);

public class ScheduleAppointmentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/appointment/", async (ScheduleAppointmentRequest request, AppointmentDbContext dbContext) =>
        {
            bool appointmentExists = await dbContext.Appointments.AnyAsync(a =>
                    a.PractitionerId == request.PractitionerId &&
                    a.CustomerId == request.CustomerId &&
                    a.ServiceId == request.ServiceId &&
                    a.StartTime == request.StartTime);

            if (appointmentExists)
                return Results.Conflict(new { ErrorMessage = $"An appointment for the specified time already was scheduled." });

            var appointment = Entities::Appointment.Schedule(
                    Guid.NewGuid(),
                    request.PractitionerId,
                    request.CustomerId,
                    request.ServiceId,
                    request.StartTime);

            dbContext.Add(appointment);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/api/appointments/{appointment.Id}", appointment);
        });
    }
}
