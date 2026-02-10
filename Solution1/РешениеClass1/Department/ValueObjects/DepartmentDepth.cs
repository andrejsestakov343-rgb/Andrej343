using System;
using System.Collections.Generic;
using System.Text;

namespace РешениеClass1.Departament.ValueObjects
{
    public sealed record DepartmentDepth
    {
        public const short MaxDepth = 10;

        public short Value { get; }

        private DepartmentDepth(short value)
        {
            Value = value; 
        }

<<<<<<< HEAD
        public static DepartmentDepth From(short value)
=======
        public static DepartmentDepth Create(short value)
>>>>>>> d6a98d7 (world)
        {
            if (value < 0)
                throw new ArgumentException("Глубина подразделения не может быть отрицательной.", nameof(value));

            if (value > MaxDepth)
                throw new ArgumentException($"Глубина подразделения не может превышать {MaxDepth}.", nameof(value));

            return new DepartmentDepth(value);
        }

<<<<<<< HEAD
        public static DepartmentDepth From (DepartmentPath path)
        {
            var depth = path.CalculateDepth();
            return From(depth);
=======
        public static DepartmentDepth CalculateFromPath(DepartmentPath path)
        {
            var depth = path.CalculateDepth();
            return Create(depth);
>>>>>>> d6a98d7 (world)
        }

        public DepartmentDepth Increment()
        {
<<<<<<< HEAD
            return From ((short)(Value + 1));
        }

        public static DepartmentDepth From(string v)
        {
            throw new NotImplementedException();
=======
            return Create ((short)(Value + 1));
>>>>>>> d6a98d7 (world)
        }
    }
}
