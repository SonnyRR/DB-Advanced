namespace EmployeesMapping.App.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using CustomMapper;
    using EmployeesMapping.App.Commands.Contracts;
    using EmployeesMapping.App.DTO;
    using EmployeesMapping.Data;
    using EmployeesMapping.Models;
    using Microsoft.EntityFrameworkCore;

    public class ManagerInfoCommand : ICommand
    {
        private readonly Mapper _mapper;
        private readonly EmployeesMappingContext _context;

        public ManagerInfoCommand(Mapper mapper, EmployeesMappingContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public string Execute(string[] args)
        {
            int desiredManagerId = int.Parse(args[0]);

            Employee manager = this._context
                .Employees
                .Include(x => x.ManagedEmployees)
                .FirstOrDefault(x => x.Id == desiredManagerId);

            if (manager is null)
            {
                throw new ArgumentException("Invalid employee Id!");
            }

            var managerDto = this._mapper.Map<ManagerDto>(manager);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"{managerDto.FullName} | Employees {managerDto.ManagedEmployees.Count}");

            foreach (var empDto in managerDto.ManagedEmployees)
            {
                builder.AppendLine($"- {empDto.FullName} - ${empDto.Salary:F2}");
            }

            return builder.ToString().TrimEnd();
        }
    }
}
