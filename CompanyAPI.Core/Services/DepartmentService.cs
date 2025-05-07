using CompanyAPI.Core.DTOs;
using CompanyAPI.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            // Maparea datelor - simulează procesarea datelor în serviciu
            return departments.Select(d => new DepartmentWithEmployeesDTO
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
                    FullName = $"{e.FirstName} {e.LastName}" // Procesare de date
                }) ?? Enumerable.Empty<EmployeeDTO>()
            });
        }

        public async Task<DepartmentWithEmployeesDTO> GetDepartmentByIdWithEmployeesAsync(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdWithEmployeesAsync(id);

            if (department == null)
                return null;

            // Procesarea datelor extrase din baza de date
            return new DepartmentWithEmployeesDTO
            {
                Id = department.Id,
                Name = department.Name,
                Location = department.Location,
                Description = department.Description,
                // Calcule în timpul procesării datelor
                EmployeeCount = department.Employees.Count(),
                TotalSalaryBudget = department.Employees.Sum(e => e.Salary),
                Employees = department.Employees.Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    HireDate = e.HireDate,
                    Salary = e.Salary,
                    Position = e.Position,
                    FullName = $"{e.FirstName} {e.LastName}" // Procesare de date
                })
            };
        }
    }
}