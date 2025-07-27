using Appointment.Infrastructure.Data;
using Carter;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Api.Appointment.GetAppointments;

public record GetAppointmentsRequest(int PageNumber, int PageSize);

public class GetAppointmentsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/appointments", async (GetAppointmentsRequest request, AppointmentDbContext dbContext) =>
        {
            var appointments = await dbContext.Appointments
                .AsNoTracking()
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return Results.Json(appointments);
        });
    }
}
