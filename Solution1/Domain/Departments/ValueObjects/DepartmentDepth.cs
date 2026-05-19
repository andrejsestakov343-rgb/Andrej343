using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Departments.ValueObjects
{
    public sealed record DepartmentDepth
    {
        public const short MaxDepth = 10;

        public short Value { get; }

        private DepartmentDepth(short value)
        {
            Value = value;
        }

        public DepartmentDepth Increment()
        {
            return new DepartmentDepth((short)(Value + 1));
        }


        public static DepartmentDepth Create(short value)

        {
            if (value < 0)
                throw new ArgumentException("Глубина подразделения не может быть отрицательной.", nameof(value));

            if (value > MaxDepth)
                throw new ArgumentException($"Глубина подразделения не может превышать {MaxDepth}.", nameof(value));

            return new DepartmentDepth(value);
        }

        public static DepartmentDepth Create(int value)
        {
            if ((value < 0) || (value > MaxDepth))
                throw new ArgumentException($"Значение должно быть в диапазоне от {short.MinValue} до {short.MaxValue}", nameof(value));

            return Create(value);
        }

    }
}
