using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Globalization;
=======
>>>>>>> d6a98d7 (world)
using System.Text;

namespace РешениеClass1.Position.ValueObjects
{
<<<<<<< HEAD
    public sealed record PositionName
    {
        private PositionName(string Value)
        {
            Name = Value;
        }
        public string Name { get; }
=======
    public sealed record PositionName(string Value)
    {
>>>>>>> d6a98d7 (world)
        private const int MinLength = 1;
        private const int MaxLength = 100;

        // Фабрика — единственный способ создания
        public static PositionName From(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Position name cannot be null, empty, or whitespace.", nameof(value));

            if (value.Length > MaxLength)
                throw new ArgumentException($"Position name cannot exceed {MaxLength} characters.", nameof(value));

            return new PositionName(value.Trim());
        }

        // Неявные преобразования
        public static implicit operator string(PositionName name) => name.Value;
        public static implicit operator PositionName(string name) => From(name);
    }
}
