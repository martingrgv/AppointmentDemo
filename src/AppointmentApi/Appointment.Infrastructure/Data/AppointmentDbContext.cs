using Entities = Appointment.Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Infrastructure.Data;

public class AppointmentDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Entities::Appointment> Appointments => Set<Entities::Appointment>();
    public DbSet<Entities::Practitioner> Practitioners => Set<Entities::Practitioner>();
    public DbSet<Entities::Customer> Customers => Set<Entities::Customer>();
    public DbSet<Entities::Service> Services => Set<Entities::Service>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentDbContext).Assembly);
    }
}
