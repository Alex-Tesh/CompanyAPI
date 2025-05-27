using CompanyAPI.Database.Entities;

namespace CompanyAPI.Database.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdWithEmployeesAsync(int id);
        Task SaveChangesAsync();
    }
}
