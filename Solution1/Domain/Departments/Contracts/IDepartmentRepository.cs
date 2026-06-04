namespace Domain.Departments.Contracts;

public interface IDepartmentRepository
{
    Task<Department?> GetById(Guid id, CancellationToken ct = default);
    Task Delete(Department department, CancellationToken ct = default);
    Task DeleteMany(IEnumerable<Department> departments, CancellationToken ct = default);
}
