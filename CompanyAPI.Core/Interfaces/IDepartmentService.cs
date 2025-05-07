using CompanyAPI.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyAPI.Core.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentWithEmployeesDTO>> GetAllDepartmentsAsync();
        Task<DepartmentWithEmployeesDTO> GetDepartmentByIdWithEmployeesAsync(int id);
    }
}