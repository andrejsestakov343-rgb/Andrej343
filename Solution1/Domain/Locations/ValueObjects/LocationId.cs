namespace Domain.Locations.ValueObjects
{
    public sealed record LocationId
    {
        public Guid Value { get; }

        public LocationId(Guid value)
        {
            Value = value;
        }

        public static LocationId Create()
        {
            return new LocationId(Guid.NewGuid());
        }

        public static LocationId Create(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Идентификатор не может быть пустым.", nameof(value));

            return new LocationId(value);
        }
        public static implicit operator LocationId(Guid guid)  => new(guid);
    }
}
