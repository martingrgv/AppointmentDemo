namespace AppointmentDomain.Domain.ValueObjects;

public record WorkingHour(DayOfWeek Day, TimeSpan Start, TimeSpan End)
{
    public bool Includes(TimeSpan time, TimeSpan duration)
        => time >= Start && time + duration <= End;
}