using System;

namespace EmployeesMapping.App.DTO
{
    public class EmployeeProjectionDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public ManagerProjectionDto Manager { get; set; }
    }
}
