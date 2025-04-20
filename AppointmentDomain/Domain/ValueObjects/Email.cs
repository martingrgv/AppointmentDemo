namespace AppointmentDomain.Domain.ValueObjects;

public record Email
{
    private static readonly char[] SpecialSymbols = [ '@', '.'];
    
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
    
    public static Email Create(string value)
    {
        if (SpecialSymbols.All(s => !value.Contains(s)))
        {
            throw new InvalidOperationException("Invalid email format");
        }
        
        return new Email(value);
    }
}