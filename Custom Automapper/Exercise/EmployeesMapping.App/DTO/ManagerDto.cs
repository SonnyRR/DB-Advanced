namespace EmployeesMapping.App.DTO
{
    using System.Collections.Generic;

    using EmployeesMapping.Models;

    public class ManagerDto
    {
        public ManagerDto()
        {
            this.ManagedEmployees = new List<EmployeeDto>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public List<EmployeeDto> ManagedEmployees { get; set; }
    }
}
