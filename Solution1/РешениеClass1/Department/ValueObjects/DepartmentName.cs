using System;
using System.Collections.Generic;
using System.Text;

namespace РешениеClass1.Departament.ValueObjects
{
    public sealed record DepartmentName
    {
        public const int MaxLength = 128;
        public const int MinLength = 2;

        public string Value { get; }

        private DepartmentName(string  value) => Value = value;

        public static DepartmentName Create (string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Название подразделения не может быть пустым.", nameof(value));

            if (value.Length > MaxLength)
                throw new ArgumentException($"Название подразделения не может быть превышать {MaxLength} символов.", nameof(value));

            if (value.Length < MinLength)
                throw new ArgumentException($"Название подраздения должен быть от {MinLength} до {MaxLength} символов.", nameof(value));

            return new DepartmentName(value);
        }
    }
}
