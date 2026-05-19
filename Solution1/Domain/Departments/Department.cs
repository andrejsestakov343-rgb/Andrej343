using System.Data.Common;
using Domain.Departments.ValueObjects;

namespace Domain.Departments
{
    public class Department
    {
        public Department()
        {
            
        }
        public DepartmentId Id { get;  }
        public DepartmentName Name { get; }
        public DepartmentIdentifier Identifier { get; }
        public DepartmentId ParentId { get; set; }
        public DepartmentPath Path { get; }
        public DepartmentDepth Depth { get; }
        public bool IsActive { get; }
        public EntityLifeTime LifeTime { get; }

        public ICollection<DepartmentPosition> DepartmentPositions { get;  } = new List<DepartmentPosition>();
        public ICollection<DepartmentLocation> DepartmentLocations { get;  } = new List<DepartmentLocation>();

        private Department(

            DepartmentId id,
            DepartmentName name,
            DepartmentIdentifier identifier,
            DepartmentId parentId,
            DepartmentPath path,
            DepartmentDepth depth,
            EntityLifeTime lifeTime)
        {
            Id = id;
            Name = name;
            Identifier = identifier;
            ParentId = parentId;
            Path = path;
            Depth = depth;
            LifeTime = lifeTime;
        }

        public static Department CreateRoot(
            DepartmentName name,
            DepartmentIdentifier identifier,
            bool isActive = true)
        {
            var id = DepartmentId.Create();
            var path = DepartmentPath.CreateForRoot(identifier.Value);
            var depth = DepartmentDepth.Create(1);
            var lifeTime = new EntityLifeTime();

            return new Department(id, name, identifier, null, path, depth, lifeTime);
        }

        public static Department CreateChild(
            DepartmentName name,
            DepartmentIdentifier identifier,
            Department parent,
            bool isActive = true)
        {

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (identifier == null)
                throw new ArgumentNullException(nameof(identifier));

            if (parent == null)
            throw new ArgumentNullException(nameof(parent));

                if (!parent.IsActive)
                throw new InvalidOperationException("Нельзя создать дочернее в архивном подразделении");

            if (parent.Depth.Value >= DepartmentDepth.MaxDepth)
                throw new InvalidOperationException($"Превышена максимальная глубина: {DepartmentDepth.MaxDepth}");


            var id = DepartmentId.Create();
            // parent_path + . + child_identifier

            var path = DepartmentPath.CreateForChild(parent.Path, identifier.Value);


            // massiv strok = .Split('.')
            // length = depth
            var depth = parent.Depth.Increment();
            var lifeTime = new EntityLifeTime();

            return new Department(id, name, identifier, parent.Id, path, depth, lifeTime);
        }
        public void AddChild(Department child)
        {
            if (child == null)
                throw new ArgumentNullException(nameof(child));

            if (!IsActive)
                throw new InvalidOperationException("Нельзя добавить дочернее в архивное подразделение");

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
    }
}
















