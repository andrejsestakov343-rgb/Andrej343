using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain.Positions.ValueObjects
{
    public sealed record PositionName
    {
        private PositionName(string Value)
        {
            Name = Value;
            this.Value = Value;
        }

        public string Name { get; }
        public string Value { get; private set; }

        private const int MinLength = 1;
        private const int MaxLength = 100;


        public static PositionName From(string value)
        {
        if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("Position name cannot be null, empty, or whitespace.",nameof(value));

        if (value.Length > MaxLength)
        throw new ArgumentException($"Position name cannot exceed {MaxLength} characters.", nameof(value));

            return new PositionName(value.Trim());
        }

        public static implicit operator string(PositionName name) => name.Value;

        public static implicit operator PositionName(string name) => From(name);
    }
}
