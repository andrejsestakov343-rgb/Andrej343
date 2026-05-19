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

        }

        public string Name { get; }

        private const int MinLength = 1;
        private const int MaxLength = 100;


        public static PositionName Create(string value)
        {
        if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("Position name cannot be null, empty, or whitespace.",nameof(value));

        if (value.Length > MaxLength)
        throw new ArgumentException($"Position name cannot exceed {MaxLength} characters.", nameof(value));

            return new PositionName(value.Trim());
        }

    }
}
