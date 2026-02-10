namespace РешениеClass1.Position.ValueObjects
{

    public sealed record PositionId
    {
        private PositionId(Guid Value)
        {
            Id = Value;
        }

        public Guid Id { get; }

        public Guid Value { get; private set; }


        public static PositionId New() => new(Guid.NewGuid());

        public static implicit operator Guid(PositionId id) => id.Value;
        public static implicit operator PositionId(Guid guid) => new(guid);

        public override string ToString() => Value.ToString();
    }
}
