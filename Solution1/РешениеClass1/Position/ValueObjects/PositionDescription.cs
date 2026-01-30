using System;
using System.Collections.Generic;
using System.Text;
using РешениеClass1.Position.ValueObjects;

namespace РешениеClass1.Position.ValueObjects
{
    public sealed record PositionDescription(string? Value)
    {
        private const int MaxLength = 500;

        // Фабрика — единственный способ создания с валидацией
        public static PositionDescription From(string? value)
        {
            if (value is not null && value.Length > MaxLength)
                throw new ArgumentException(
                    $"Position description cannot exceed {MaxLength} characters.",
                    nameof(value));

            return new PositionDescription(value?.Trim());
        }

        // Неявные преобразования
        public static implicit operator string?(PositionDescription desc) => desc.Value;
        public static implicit operator PositionDescription?(string? desc) =>
            desc is null ? null : From(desc);
    }
}

