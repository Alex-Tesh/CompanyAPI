using CompanyAPI.Core.DTOs;
using CompanyAPI.Core.Interfaces;
using CompanyAPI.Database.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Database.Interfaces;


namespace CompanyAPI.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentWithEmployeesDTO>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return departments.Select(d => MapToDTO(d));

        }

        public async Task<DepartmentWithEmployeesDTO> GetDepartmentByIdWithEmployeesAsync(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdWithEmployeesAsync(id);
            if (department == null) return null;
            return MapToDTO(department);
        }

        public async Task<(bool Success, string Message, DepartmentWithEmployeesDTO? Data)> UpdateDepartmentAsync(int id, DepartmentWithEmployeesDTO updatedDept)
        {
            var department = await _departmentRepository.GetDepartmentByIdWithEmployeesAsync(id);
            if (department == null)
                return (false, "Departamentul nu a fost găsit.", null);

            department.Name = updatedDept.Name;
            department.Location = updatedDept.Location;
            department.Description = updatedDept.Description;

            await _departmentRepository.SaveChangesAsync();

            return (true, "Departamentul a fost actualizat.", MapToDTO(department));
        }

        private DepartmentWithEmployeesDTO MapToDTO(Department d)
        {
            return new DepartmentWithEmployeesDTO
            {
                Id = d.Id,
                Name = d.Name,
                Location = d.Location,
                Description = d.Description,
                EmployeeCount = d.Employees?.Count() ?? 0,
                TotalSalaryBudget = d.Employees?.Sum(e => e.Salary) ?? 0,
                Employees = d.Employees?.Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    HireDate = e.HireDate,
                    Salary = e.Salary,
                    Position = e.Position,
                    FullName = $"{e.FirstName} {e.LastName}"
                }) ?? Enumerable.Empty<EmployeeDTO>()
            };
        }
    }
}
