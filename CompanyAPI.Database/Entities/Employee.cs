using System;

namespace CompanyAPI.Database.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }

        // Foreign key pentru relatia cu Department
        public int DepartmentId { get; set; }

        // Navigation property
        public virtual Department Department { get; set; }
    }
}