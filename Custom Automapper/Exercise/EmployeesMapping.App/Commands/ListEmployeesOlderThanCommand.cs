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
    using Microsoft.EntityFrameworkCore;

    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly Mapper _mapper;
        private readonly EmployeesMappingContext _context;

        public ListEmployeesOlderThanCommand(Mapper mapper, EmployeesMappingContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public string Execute(string[] args)
        {
            int age = int.Parse(args[0]);

            var employees = this._context
                .Employees
                .Include(x => x.Manager)
                .Where(x => DateTime.Now.Year - x.BirthDay.Value.Year > age)
                .OrderByDescending(x => x.Salary)
                .ToList();

            StringBuilder builder = new StringBuilder();

            var mapped = new List<EmployeeProjectionDto>();

            foreach (var emp in employees)
            {
                var empMapped = this._mapper.Map<EmployeeProjectionDto>(emp);

                //mapped.Add(empMapped);
            }           

            return string.Empty;
        }
    }
}
