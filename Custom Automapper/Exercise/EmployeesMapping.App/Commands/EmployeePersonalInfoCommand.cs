namespace EmployeesMapping.App.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using CustomMapper;
    using EmployeesMapping.App.Commands.Contracts;
    using EmployeesMapping.Data;

    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly Mapper _mapper;
        private readonly EmployeesMappingContext _context;

        public EmployeePersonalInfoCommand(Mapper mapper, EmployeesMappingContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public string Execute(string[] args)
        {
            var desiredId = int.Parse(args[0]);

            var employee = this._context
                .Employees
                .FirstOrDefault(x => x.Id == desiredId);

            if (employee is null)
            {
                throw new ArgumentException("Invalid employee id!");
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"ID: {employee.Id} - {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}");

            string birthDay = employee.BirthDay.HasValue 
                ? employee.BirthDay.Value.ToString("dd-MM-yyyy") 
                : "N/A";

            builder.AppendLine($"Birthday: {birthDay}");

            string address = employee.Address is null 
                ? "N/A" 
                : employee.Address;

            builder.AppendLine($"Address: {address}");

            return builder.ToString();
        }
    }
}
