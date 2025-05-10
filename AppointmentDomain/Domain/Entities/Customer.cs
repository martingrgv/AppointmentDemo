using AppointmentDomain.Domain.Abstraction;

namespace AppointmentDomain.Domain.Entities;

public class Customer : Entity
{
    private Customer(Guid id, string firstName, string lastName, string email, string phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }

    public static Customer Create(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string phoneNumber)
        => new Customer(id, firstName, lastName, email, phoneNumber);
}
