using Appointment.Infrastructure.Data;
using Carter;

namespace Appointment.Api.Appointment.GetAppointmentsByCustomer;

public record GetAppointmentsByCustomerRequest(Guid Id);

public class GetAppointmentsByCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/appointments/customer/{id}", async (GetAppointmentsByCustomerRequest request, AppointmentDbContext dbContext) =>
        {
            var customer = await dbContext.Appointments.FindAsync(request.Id);
            return Results.Json(customer);
        });
    }
}
