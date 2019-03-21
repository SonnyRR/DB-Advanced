namespace EmployeesMapping.App.DTO
{
    using System.Collections.Generic;

    public class ManagerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection <EmployeeDto> Employees { get; set; }
    }
}
