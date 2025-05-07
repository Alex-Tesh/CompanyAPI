using System;
using System.Collections.Generic;

namespace CompanyAPI.Core.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }
        public string FullName { get; set; } // Câmp calculat în procesare
    }

    public class DepartmentWithEmployeesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int EmployeeCount { get; set; } // Câmp calculat în procesare
        public decimal TotalSalaryBudget { get; set; } // Câmp calculat în procesare
        public IEnumerable<EmployeeDTO> Employees { get; set; }
    }
}