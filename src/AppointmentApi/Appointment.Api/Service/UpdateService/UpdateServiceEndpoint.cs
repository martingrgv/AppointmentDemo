using Appointment.Infrastructure.Data;
using Carter;

namespace Appointment.Api.Service.UpdateService;

public record UpdateServiceRequest(Guid Id, string Name, TimeSpan Duration, decimal Price);

public class UpdateServiceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/services/{id}", async (UpdateServiceRequest request, AppointmentDbContext dbContext) =>
        {
            var service = await dbContext.Services.FindAsync(request.Id);

            if (service == null)
                return Results.BadRequest("Service was not found!");

            service.Update(request.Name, request.Duration, request.Price);
            await dbContext.SaveChangesAsync();

            return Results.Ok();
        });
    }
}
