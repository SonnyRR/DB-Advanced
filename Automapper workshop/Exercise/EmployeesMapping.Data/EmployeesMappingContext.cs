namespace EmployeesMapping.Data
{
    using System;
    using System.Runtime.InteropServices;
    using EmployeesMapping.Models;
    using Microsoft.EntityFrameworkCore;

    public class EmployeesMappingContext : DbContext
    {
        public EmployeesMappingContext(DbContextOptions options)
            : base(options)
        {
        }

        public EmployeesMappingContext()
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    optionsBuilder.UseSqlServer(ConnectionInfo.ConnectionStringWindows);

                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    optionsBuilder.UseSqlServer(ConnectionInfo.ConnectionStringMacOS);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "Georgi", LastName = "Georgiev", Salary = 12131.44M },
                new Employee { Id = 2, FirstName = "Maria", LastName = "Marieva", Salary = 999.10M, BirthDay = DateTime.Now.AddDays(-15), Address = "Neznam"},
                new Employee { Id = 3, FirstName = "Alisia", LastName = "Alisieva", Salary = 11111.11M},
                new Employee { Id = 4, FirstName = "Pesho", LastName = "Peshov", Salary = 431.44M, Address = "Neznam2"},
                new Employee { Id = 6, FirstName = "Miro", LastName = "Mirov", Salary = 2000.44M, BirthDay = DateTime.Now.AddDays(-365)},
                new Employee { Id = 7, FirstName = "Blago", LastName = "Petkov", Salary = 2000.44M, BirthDay = DateTime.Now.AddDays(-3000)},
                new Employee { Id = 8, FirstName = "Emanuela", LastName = "Marinova", Salary = 2000.44M, BirthDay = DateTime.Now.AddDays(-4000)},
                new Employee { Id = 9, FirstName = "Koce", LastName = "Kocev", Salary = 2000.44M, BirthDay = DateTime.Now.AddDays(-2000)}
                );
                
        }
    }
}
