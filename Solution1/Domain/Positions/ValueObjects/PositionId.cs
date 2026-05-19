namespace Domain.Positions.ValueObjects
{
    public sealed record PositionId
    {
        public PositionId(Guid Value)
        {
            Id = Value;
        }

        public Guid Id { get; }

        public Guid Value { get; private set; }

        public static PositionId New() => new(Guid.NewGuid());


        public override string ToString() => Value.ToString();
    }
}
