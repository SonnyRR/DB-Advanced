namespace EmployeesMapping.App.Commands
{    
    using CustomMapper;
    using EmployeesMapping.App.Commands.Contracts;
    using EmployeesMapping.App.DTO;
    using EmployeesMapping.Data;
    using EmployeesMapping.Models;

    public class AddEmployeeCommand : ICommand
    {
        private readonly EmployeesMappingContext _context;
        private readonly Mapper _mapper;

        public AddEmployeeCommand(Mapper mapper, EmployeesMappingContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public string Execute(string[] args)
        {
            // There is no validation specified in exercise requirements.
            string firstName = args[0];
            string lastName = args[1];
            decimal salary = decimal.Parse(args[2]);

            Employee employee = new Employee(firstName, lastName, salary);
            this._context.Employees.Add(employee);

            this._context.SaveChanges();

            EmployeeDto employeeDto = this._mapper.Map<EmployeeDto>(employee);

            return $"Successfully registered user and dto object: {employeeDto.FirstName} {employeeDto.LastName} {employee.Salary:F2}";
        }
    }
}
