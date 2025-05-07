using CompanyAPI.Database.Data;
using CompanyAPI.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyAPI.Database.Repositories
{
    public class DepartmentRepository
    {
        private readonly CompanyDbContext _context;

        public DepartmentRepository(CompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        // Implementarea metodei care extrage intrările din tabelul A (Department) 
        // incluzând și informațiile din tabelul B (Employee) pe baza relației one-to-many
        public async Task<Department> GetDepartmentByIdWithEmployeesAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.Employees)  // Include realizează join-ul între tabele
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}