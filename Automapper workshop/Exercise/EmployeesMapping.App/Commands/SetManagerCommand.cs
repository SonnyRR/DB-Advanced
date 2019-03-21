namespace EmployeesMapping.App.Commands
{
    using System;
    using System.Linq;
    using CustomMapper;
    using EmployeesMapping.App.Commands.Contracts;
    using EmployeesMapping.App.DTO;
    using EmployeesMapping.Data;

    public class SetManagerCommand : ICommand
    {

        private readonly EmployeesMappingContext _context;

        public SetManagerCommand(EmployeesMappingContext context)
        {
            this._context = context;
        }

        public string Execute(string[] args)
        {
            int employeeId = int.Parse(args[0]);
            int managerId = int.Parse(args[1]);

            var employee = this._context
                .Employees
                .FirstOrDefault(x => x.Id == employeeId);

            if (employee is null)
            {
                throw new ArgumentException("Invalid employee Id!");
            }

            var manager = this._context
                .Employees
                .FirstOrDefault(x => x.Id == managerId);

            if (manager is null)
            {
                throw new ArgumentException("Invalid manager Id!");
            }

            manager.ManagedEmployees.Add(employee);
            this._context.SaveChanges();
            
            return $"Successfully added {employee.FirstName} {employee.LastName} under {manager.FirstName} {manager.LastName}'s management.";
        }
    }
}
