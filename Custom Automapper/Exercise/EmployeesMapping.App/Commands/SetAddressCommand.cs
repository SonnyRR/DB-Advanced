namespace EmployeesMapping.App.Commands
{
    using System;
    using System.Linq;
    using CustomMapper;
    using EmployeesMapping.App.Commands.Contracts;
    using EmployeesMapping.App.DTO;
    using EmployeesMapping.Data;

    public class SetAddressCommand : ICommand
    {
        private readonly Mapper _mapper;
        private readonly EmployeesMappingContext _context;

        public SetAddressCommand(Mapper mapper, EmployeesMappingContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public string Execute(string[] args)
        {
            int desiredEmployeeId = int.Parse(args[0]);
            string address = args[1];

            var employee = this._context
                .Employees
                .FirstOrDefault(x => x.Id == desiredEmployeeId);

            if (employee is null)
            {
                throw new ArgumentException("Invalid employee id!");
            }

            employee.Address = address;
            this._context.SaveChanges();


            var employeeDto = this._mapper.Map<EmployeeDto>(employee);            

            return $"Successfully updated employee address {employee.Address} with id {desiredEmployeeId} -> {employeeDto.FirstName} {employeeDto.LastName}";
        }
    }
}
