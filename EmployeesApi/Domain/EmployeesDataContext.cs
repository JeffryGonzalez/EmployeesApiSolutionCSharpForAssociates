using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApi.Domain
{
    public class EmployeesDataContext :DbContext
    {
        public EmployeesDataContext(DbContextOptions<EmployeesDataContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(e => e.FirstName).HasMaxLength(200);
            modelBuilder.Entity<Employee>().Property(e => e.LastName).HasMaxLength(200);
            modelBuilder.Entity<Employee>().Property(e => e.Department).HasMaxLength(20);

            modelBuilder.Entity<Employee>().HasData(
                    new Employee {  Id = 1, FirstName="Sue", LastName = "Jones", Department="CEO", IsActive=true},
                    new Employee {  Id = 2, FirstName="Bob", LastName = "Smith", Department="DEV", IsActive=true},
                    new Employee {  Id = 3, FirstName="Sean", LastName="Carlin", Department="QA", IsActive=false}

                );
        }
    }
}
