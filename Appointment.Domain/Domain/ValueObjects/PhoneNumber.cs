namespace Appointment.Domain.Domain.ValueObjects;

public record PhoneNumber
{
    private const int Length = 13;
    private static readonly char[] SpecialSymbols = [ '+' ];
    
    private PhoneNumber(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static PhoneNumber Create(string value)
    {
        if (value.Length != Length)
        {
            throw new ArgumentException("Invalid length");
        }

        if (SpecialSymbols.All(s => !value.Contains(s)))
        {
            throw new ArgumentException("Invalid special symbol");
        }

        return new PhoneNumber(value);
    }
}