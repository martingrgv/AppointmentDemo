using Entities = Appointment.Domain.Domain.Entities;
using Appointment.Infrastructure.Data;
using Carter;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Api.Service.CreateService;

public record CreateServiceRequest(string Name, TimeSpan Duration, decimal Price);

public class CreateServiceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/services", async (CreateServiceRequest request, AppointmentDbContext dbContext) =>
        {
            bool serviceExists = await dbContext.Services.AnyAsync(s =>
                s.Name == request.Name &&
                s.Duration == request.Duration &&
                s.Price == request.Price);

            if (serviceExists)
                return Results.BadRequest(new { ErrorMessage = "Service already exists!" });

            var service = Entities::Service.Create(
                Guid.NewGuid(),
                request.Name,
                request.Duration,
                request.Price);

            dbContext.Add(service);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/api/services/{service.Id}", service);
        });
    }
}
