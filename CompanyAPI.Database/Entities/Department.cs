using System.Collections.Generic;

namespace CompanyAPI.Database.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        // Relație one-to-many: un departament poate avea mai multi angajați
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}