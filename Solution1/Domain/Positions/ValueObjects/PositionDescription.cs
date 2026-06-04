namespace Domain.Positions.ValueObjects
{
    public sealed record PositionDescription
    {
        private PositionDescription(string value)
        {
            Value = value;
        }


        public string Value { get; }
        private const int MaxLength = 500;

        public static PositionDescription Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Значение не может быть пустым", nameof(value));
            }

            if (value.Length > MaxLength)
            {
                throw new ArgumentException("Значение слишком длинное", nameof(value));
            }

            return new PositionDescription(value.Trim());
        }
         public static implicit operator string(PositionDescription desc) => desc.Value;
         public static implicit operator PositionDescription(string value) => Create(value);
    }
}
