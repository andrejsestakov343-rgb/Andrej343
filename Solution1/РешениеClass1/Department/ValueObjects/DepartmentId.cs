using System;
using System.Collections.Generic;
using System.Text;

namespace РешениеClass1.Departament.ValueObjects
{
    public sealed record DepartmentId
    {
        public Guid Value { get; }

        private DepartmentId(Guid value) => Value = value;

        public static DepartmentId Create() => new(Guid.NewGuid());

        public static DepartmentId Create(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Идентификатор подразделения не может быть пустым.", nameof(value));

            return new DepartmentId(value);
        }

<<<<<<< HEAD
        public static DepartmentId? From(Guid? value)
=======
        public static DepartmentId? CreateNullable(Guid? value)
>>>>>>> d6a98d7 (world)
        {
            if (value == null || value == Guid.Empty)
                return null;

            return Create(value.Value);
        }
    }
}
