namespace EmployeesMapping.App.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public decimal Salary { get; set; }        
    }
}
