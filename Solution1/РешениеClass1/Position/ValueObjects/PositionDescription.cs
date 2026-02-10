using System;
using System.Collections.Generic;
using System.Text;
using РешениеClass1.Position.ValueObjects;

namespace РешениеClass1.Position.ValueObjects
{
    public sealed record PositionDescription
    {
        private PositionDescription(string Value)
        {
            Description = Value;
        }

        public string Description { get; }

        public string? Value { get; private set; }

        private const int MaxLength = 500;


        // Фабрика — единственный способ создания с валидацией
        public static PositionDescription From(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Значение не может быть пустым", nameof(value));
            }

            if (value is not null && value.Length > MaxLength)
            {
                throw new ArgumentException(
                    $"Позиция не может превышать {MaxLength} символов.",
                    nameof(value));
            }

            return new PositionDescription(value.Trim());
        }

        // Неявные преобразования
        public static implicit operator string?(PositionDescription desc) => desc.Value;
        public static implicit operator PositionDescription?(string? desc) =>
            desc is null ? null : From(desc);
    }
}



