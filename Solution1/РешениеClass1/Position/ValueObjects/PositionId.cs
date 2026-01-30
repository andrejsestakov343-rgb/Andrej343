using System;
using System.Collections.Generic;
using System.Text;

namespace РешениеClass1.Position.ValueObjects
{
<<<<<<< HEAD
        public sealed record PositionId
        {
        private PositionId(Guid Value)
        {
            Id = Value;
        }
        public Guid Id { get; }
=======
        public sealed record PositionId(Guid Value)
        {
>>>>>>> d6a98d7 (world)
            public static PositionId New() => new(Guid.NewGuid());

            public static implicit operator Guid(PositionId id) => id.Value;
            public static implicit operator PositionId(Guid guid) => new(guid);

            public override string ToString() => Value.ToString();
        }
}
