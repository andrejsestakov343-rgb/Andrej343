using System;
using System.Collections.Generic;
using System.Text;

namespace РешениеClass1.Position.ValueObjects
{
        public sealed record PositionId(Guid Value)
        {
            public static PositionId New() => new(Guid.NewGuid());

            public static implicit operator Guid(PositionId id) => id.Value;
            public static implicit operator PositionId(Guid guid) => new(guid);

            public override string ToString() => Value.ToString();
        }
}
