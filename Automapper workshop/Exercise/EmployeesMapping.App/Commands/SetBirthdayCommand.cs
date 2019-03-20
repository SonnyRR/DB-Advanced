namespace EmployeesMapping.App.Commands
{
    using System;
    using System.Globalization;
    using System.Linq;

    using CustomMapper;
    using EmployeesMapping.App.Commands.Contracts;
    using EmployeesMapping.App.DTO;
    using EmployeesMapping.Data;

    public class SetBirthdayCommand : ICommand
    {
        private readonly EmployeesMappingContext _context;
        private readonly Mapper _mapper;
        private const string DATE_FORMAT = "dd-MM-yyyy";

        public SetBirthdayCommand(Mapper mapper, EmployeesMappingContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public string Execute(string[] args)
        {
            int desiredEmployeeId = int.Parse(args[0]);
            DateTime birthday = DateTime.ParseExact(args[1], DATE_FORMAT, CultureInfo.InvariantCulture);

            var employee = this._context
                .Employees
                .FirstOrDefault(e => e.Id == desiredEmployeeId);

            if (employee is null)
            {
                throw new ArgumentException("Invalid employee id!");
            }

            employee.BirthDay = birthday;
            this._context.SaveChanges();

            var employeeDto = this._mapper.Map<EmployeeDto>(employee);

            return $"Successfully updated employee birthday {employee.BirthDay.Value.ToString(DATE_FORMAT)} with id {desiredEmployeeId} -> {employeeDto.FirstName} {employeeDto.LastName}";

        }
    }
}
