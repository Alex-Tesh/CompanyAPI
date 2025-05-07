using CompanyAPI.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CompanyAPI.Database.Data
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurarea relatiei one-to-many
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data pentru demo
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT", Location = "Etaj 3", Description = "Dezvoltare software și infrastructură IT" },
                new Department { Id = 2, Name = "HR", Location = "Etaj 1", Description = "Resurse umane și recrutare" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Andrei",
                    LastName = "Popescu",
                    Email = "andrei.popescu@company.com",
                    HireDate = new DateTime(2020, 5, 15),
                    Salary = 6500,
                    Position = "Developer Senior",
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Maria",
                    LastName = "Ionescu",
                    Email = "maria.ionescu@company.com",
                    HireDate = new DateTime(2021, 3, 10),
                    Salary = 5800,
                    Position = "Developer",
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Elena",
                    LastName = "Dumitrescu",
                    Email = "elena.dumitrescu@company.com",
                    HireDate = new DateTime(2019, 11, 5),
                    Salary = 6200,
                    Position = "HR Manager",
                    DepartmentId = 2
                },
                new Employee
                {
                    Id = 4,
                    FirstName = "Mihai",
                    LastName = "Stanescu",
                    Email = "mihai.stanescu@company.com",
                    HireDate = new DateTime(2022, 1, 20),
                    Salary = 4500,
                    Position = "HR Specialist",
                    DepartmentId = 2
                }
            );
        }
    }
}