
using Domain.Departments;
using Domain.LocationContext.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.LocationContext
{
    public class Location
    {
        public Guid Id { get; private set; }
        public string Address { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string TimeZone { get; private set; } = null!;
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Location(Guid id, string name, string address, string timeZone, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Address = address;
            TimeZone = timeZone;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsActive = true;
        }

        private Location()
        {
        }

        public void ChangeTimeZone(string timeZone)
        {
            if (!IsActive)
                throw new InvalidOperationException("Редактирование неактивных локаций запрещено");

            TimeZone = timeZone;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeName(string name)
        {
            if (!IsActive)
                throw new InvalidOperationException("Редактирование неактивных локаций запрещено");

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeAddress(string address)
        {
            if (!IsActive)
                throw new InvalidOperationException("Редактирование неактивных локаций запрещено");

            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Локация уже неактивна");

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Локация уже активна");

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public ICollection<DepartmentLocation> DepartmentLocations { get; } = new List<DepartmentLocation>();
    }
}



