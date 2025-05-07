using CompanyAPI.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyAPI.Core.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdWithEmployeesAsync(int id);
    }
}