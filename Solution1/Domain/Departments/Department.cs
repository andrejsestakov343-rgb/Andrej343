
using Domain.Departments.ValueObjects;

namespace Domain.Departments
{
    public class Department
    {
        public Department()
        {
            
        }
        public DepartmentId Id { get; } = null!;
        public DepartmentName Name { get; private set; } = null!;
        public DepartmentIdentifier Identifier { get; } = null!;
        public DepartmentId? ParentId { get; set; }
        public DepartmentPath Path { get; } = null!;
        public DepartmentDepth Depth { get; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public ICollection<DepartmentPosition> DepartmentPositions { get; } = new List<DepartmentPosition>();
        public ICollection<DepartmentLocation> DepartmentLocations { get; } = new List<DepartmentLocation>();

        private Department(
            DepartmentId id,
            DepartmentName name,
            DepartmentIdentifier identifier,
            DepartmentId? parentId,
            DepartmentPath path,
            DepartmentDepth depth,
            DateTime createdAt,
            DateTime updatedAt,
            bool isActive)
        {
            Id = id;
            Name = name;
            Identifier = identifier;
            ParentId = parentId;
            Path = path;
            Depth = depth;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsActive = isActive;
        }

        public static Department CreateRoot(
            DepartmentName name,
            DepartmentIdentifier identifier,
            bool isActive = false)
        {
            var id = DepartmentId.Create();
            var path = DepartmentPath.CreateForRoot(identifier.Value);
            var depth = DepartmentDepth.Create(1);
            var now = DateTime.UtcNow;

            return new Department(id, name, identifier, null, path, depth, now, now, isActive);
        }

        public static Department CreateChild(
            DepartmentName name,
            DepartmentIdentifier identifier,
            Department parent,
            bool isActive = false)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (identifier == null)
                throw new ArgumentNullException(nameof(identifier));

            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            if (!parent.IsActive)
                throw new InvalidOperationException("Нельзя создать дочернее в неактивном подразделении");

            if (parent.Depth.Value >= DepartmentDepth.MaxDepth)
                throw new InvalidOperationException($"Превышена максимальная глубина: {DepartmentDepth.MaxDepth}");

            var id = DepartmentId.Create();
            var path = DepartmentPath.CreateForChild(parent.Path, identifier.Value);
            var depth = parent.Depth.Increment();
            var now = DateTime.UtcNow;

            return new Department(id, name, identifier, parent.Id, path, depth, now, now, isActive);
        }

        public void AddChild(Department child)
        {
            if (child == null)
                throw new ArgumentNullException(nameof(child));

            if (!IsActive)
                throw new InvalidOperationException("Нельзя добавить дочернее в неактивное подразделение");

            if (child.ParentId != null)
                throw new InvalidOperationException("Подразделение уже имеет родителя");

            if (child.Id == this.Id)
                throw new InvalidOperationException("Нельзя присоединить подразделение к самому себе");

            child.ParentId = this.Id;
        }

        public bool IsRoot() => ParentId == null;

        public bool IsChildOf(Department parent)
        {
            if (parent is null || Path?.Value is null || parent.Path?.Value is null)
                return false;

            return Path.Value.StartsWith(parent.Path.Value + ".", StringComparison.Ordinal);
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Подразделение уже активно");

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Подразделение уже неактивно");

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeName(DepartmentName newName)
        {
            if (!IsActive)
                throw new InvalidOperationException("Нельзя изменить имя неактивного подразделения");

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
















