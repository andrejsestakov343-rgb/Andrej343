namespace Domain.LocationContext.ValueObjects;

public sealed record class LocationAddress (string Value)
{
    public const int Max_Length = 255;
    public static LocationAddress Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Адрес не может быть пустым");

        if (address.Length > Max_Length)
            throw new ArgumentException($"Адрес не может быть длиннее {Max_Length}.");

        return new LocationAddress(address);
    }
}

