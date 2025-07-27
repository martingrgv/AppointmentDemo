using Appointment.Infrastructure.Data;
using Carter;

namespace Appointment.Api.Appointment.CancelAppointment;

public record CancelAppointmentRequest(Guid Id, string? Reason);

public class CancelAppointmentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/appointments/cancellation", async (CancelAppointmentRequest request, AppointmentDbContext dbContext) =>
        {
            var appointment = await dbContext.Appointments.FindAsync(request.Id);
            if (appointment == null)
                return Results.BadRequest(new { ErrorMessage = $"Appointment {request.Id} cannot be cancelled at this time." });

            appointment.Cancel(request.Reason);
            await dbContext.SaveChangesAsync();

            return Results.Ok();
        });
    }
}
