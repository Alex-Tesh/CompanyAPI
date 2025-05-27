using CompanyAPI.Database.Data;
using CompanyAPI.Database.Entities;
using Microsoft.EntityFrameworkCore;
using CompanyAPI.Database.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyAPI.Database.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context;

        public DepartmentRepository(CompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments
                                 .Include(d => d.Employees)
                                 .ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdWithEmployeesAsync(int id)
        {
            return await _context.Departments
                                 .Include(d => d.Employees)
                                 .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
