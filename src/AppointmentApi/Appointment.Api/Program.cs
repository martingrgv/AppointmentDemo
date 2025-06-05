using Appointment.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/appointment", ([FromBody] CreateAppointmentDto appointmentDto) =>
{
    var appointmentId = Guid.NewGuid();
    var appointment = Appointment.Domain.Domain.Entities.Appointment.Schedule(
        appointmentId,
        appointmentDto.PractitionerId,
        appointmentDto.CustomerId,
        appointmentDto.ServiceId,
        new DateTime(new DateOnly(year: 2025, month: 5, day: 11), new TimeOnly(hour: 12, minute: 30)));

    return Results.Created("/appointment", new
    {
        id = appointmentId
    });
});

app.Run();
