using Microsoft.EntityFrameworkCore;

namespace Appointment.Infrastructure.Data;

public class AppointmentDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentDbContext).Assembly);
    }
}
