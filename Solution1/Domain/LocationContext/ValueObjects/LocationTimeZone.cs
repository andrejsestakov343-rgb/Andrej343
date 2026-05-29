namespace Domain.LocationContext.ValueObjects;

public sealed record class LocationTimeZone (string Value)
{
    public const int Max_Length = 255;

    public static LocationTimeZone Create(string timeZone)
    {
        if (string.IsNullOrWhiteSpace(timeZone))
            throw new ArgumentException("Временная зона не может быть пустой");

        if (timeZone.Length > Max_Length)
            throw new ArgumentException($"Временная зона не может быть длиннее {Max_Length}");

        return new LocationTimeZone(timeZone);
    }
}

