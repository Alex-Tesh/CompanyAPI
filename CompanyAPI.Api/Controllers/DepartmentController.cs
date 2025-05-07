using CompanyAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        // Endpoint pentru listarea tuturor departamentelor
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        // Endpoint-ul care extrage date din tabelul A (Department) 
        // inclusiv cele asociate din tabelul B (Employee)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentWithEmployees(int id)
        {
            var department = await _departmentService.GetDepartmentByIdWithEmployeesAsync(id);

            if (department == null)
                return NotFound($"Departamentul cu ID-ul {id} nu a fost găsit.");

            return Ok(department);
        }
    }
}