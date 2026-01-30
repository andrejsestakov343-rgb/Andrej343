using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using РешениеClass1.Departament.ValueObjects;
using РешениеClass1.Location.ValueObjects;

namespace РешениеClass1.Departament
{
    public class Department
    {
        public DepartmentId Id { get; private set; }
        public DepartmentName Name { get; private set; }
        public DepartmentIdentifier Identifier { get; private set; }
        public DepartmentId? ParentId { get; private set; }
        public DepartmentPath Path { get; private set; }
        public DepartmentDepth Depth { get; private set; }
        public bool IsActive { get; private set; }
        public EntityLifeTime LifeTime { get; private set; }

<<<<<<< HEAD
        public Department(
=======
        private Department(
>>>>>>> d6a98d7 (world)
            DepartmentId id,
            DepartmentName name,
            DepartmentIdentifier identifier,
            DepartmentId? parentId,
            DepartmentPath path,
            DepartmentDepth depth,
            bool isActive,
            EntityLifeTime lifeTime)
        {
            Id = id;
            Name = name;
            Identifier = identifier;
            ParentId = parentId;
            Path = path;
            Depth = depth;
            IsActive = isActive;
            LifeTime = lifeTime;
        }

        public static Department CreateRoot(
            DepartmentName name,
            DepartmentIdentifier identifier,
            bool isActive = true)
        {
            var id = DepartmentId.Create();
            var path = DepartmentPath.CreateForRoot(identifier.Value); // ожидает string
            var depth = DepartmentDepth.CalculateFromPath(path);       // ожидает DepartmentPath
            var lifeTime = EntityLifeTime.Create(
                createdAt: DateTime.UtcNow,
                updatedAt: DateTime.UtcNow);

            return new Department(id, name, identifier, null, path, depth, isActive, lifeTime);
        }

        public bool IsRoot() => ParentId == null;

        public bool IsChildOf(Department parent)
        {
            if (parent is null || Path?.Value is null || parent.Path?.Value is null)
                return false;

            return Path.Value.StartsWith(parent.Path.Value + ".");
        }
    }
}
