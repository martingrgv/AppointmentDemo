using Appointment.Infrastructure.Data;
using Carter;

namespace Appointment.Api.Service.RemoveService;

public class RemoveServiceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/services/{id}", async (Guid id, AppointmentDbContext dbContext) =>
        {
            var service = await dbContext.Services.FindAsync(id);

            if (service == null)
                return Results.NotFound();

            dbContext.Remove(service);
            await dbContext.SaveChangesAsync();

            return Results.Ok();
        });
    }
}
