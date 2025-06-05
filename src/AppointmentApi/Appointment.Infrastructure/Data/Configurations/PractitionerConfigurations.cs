using Appointment.Domain.Domain.Entities;
using Appointment.Domain.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointment.Infrastructure.Data.Configurations;

public class PractitionerConfigurations : IEntityTypeConfiguration<Practitioner>
{
    public void Configure(EntityTypeBuilder<Practitioner> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(255)
            .IsRequired();

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Value)
                .HasColumnName(nameof(Email))
                .HasMaxLength(255)
                .IsRequired();
        });

        builder.OwnsOne(x => x.PhoneNumber, phone =>
        {
            phone.Property(x => x.Value)
                .HasColumnName(nameof(PhoneNumber))
                .HasMaxLength(20)
                .IsRequired();
        });

        builder.OwnsMany(x => x.WeeklySchedule, schedule =>
        {
            schedule.HasKey(x => x.Day);

            schedule.Property(x => x.Day)
                .HasConversion<string>()
                .IsRequired();

            schedule.Property(x => x.Start)
                .HasColumnType("time")
                .IsRequired();

            schedule.Property(x => x.End)
                .HasColumnType("time")
                .IsRequired();
        });

        builder.HasMany(x => x.Services)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "PractitionerServices",
                x => x.HasOne<Service>().WithMany().HasForeignKey("ServiceId"),
                x => x.HasOne<Practitioner>().WithMany().HasForeignKey("PractitionerId"),
                x =>
                {
                    x.HasKey("PractitionerId", "ServiceId");
                    x.ToTable("PractitionerServices");
                }
            );
    }
}
