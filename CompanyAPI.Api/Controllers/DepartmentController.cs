using CompanyAPI.Core.Interfaces;
using CompanyAPI.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // Endpoint GET cu filtrare, sortare si paginare
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments(
            [FromQuery] string? name,
            [FromQuery] string? location,
            [FromQuery] string? sortBy,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();

            // Filtrare
            if (!string.IsNullOrEmpty(name))
                departments = departments.Where(d => d.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(location))
                departments = departments.Where(d => d.Location.Contains(location, StringComparison.OrdinalIgnoreCase));

            // Sortare
            departments = sortBy?.ToLower() switch
            {
                "name" => departments.OrderBy(d => d.Name),
                "employeecount" => departments.OrderByDescending(d => d.EmployeeCount),
                _ => departments
            };

            // Paginare
            var paginated = departments
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(paginated);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentWithEmployees(int id)
        {
            var department = await _departmentService.GetDepartmentByIdWithEmployeesAsync(id);
            if (department == null)
                return NotFound($"Departamentul cu ID-ul {id} nu a fost găsit.");

            return Ok(department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentWithEmployeesDTO updatedDept)
        {
            var result = await _departmentService.UpdateDepartmentAsync(id, updatedDept);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Data);
        }
    }
}
