namespace Domain.PositionContext.ValueObjects;

public sealed record class PositionName (string Value)
{
    public const int MaxLength = 255;

    public static PositionName Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название позиции не может быть пустым.");
        if (name.Length > MaxLength)
            throw new ArgumentException($"Название позиции не может быть длиннее {MaxLength}.");

        return new PositionName(name);
    }
}

