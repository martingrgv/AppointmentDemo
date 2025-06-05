using Appointment.Domain.Domain.Entities;
using Appointment.Domain.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointment.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
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
    }
}
