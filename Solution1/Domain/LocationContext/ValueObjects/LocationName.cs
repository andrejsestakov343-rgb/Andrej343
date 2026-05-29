namespace Domain.LocationContext.ValueObjects;

public sealed record LocationName (string Value)
{
    public const int Max_Length = 255;
    public static LocationName Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название не может быть пустым.");

        if (name.Length > Max_Length)
            throw new ArgumentException($"Название не может быть длиннее {Max_Length}.");

         return new LocationName(name);

    }

}
