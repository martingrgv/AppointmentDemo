using Appointment.Domain.Domain.Abstraction;
using Appointment.Domain.Domain.ValueObjects;

namespace Appointment.Domain.Domain.Entities;

public class Customer : Entity
{
    private Customer(Guid id, string firstName, string lastName, Email email, PhoneNumber phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public Email Email { get; }
    public PhoneNumber PhoneNumber { get; }

    public static Customer Create(
        Guid id,
        string firstName,
        string lastName,
        Email email,
        PhoneNumber phoneNumber)
        => new(id, firstName, lastName, email, phoneNumber);
}
