namespace EmployeesMapping.App
{
    using System;
    using System.Linq;
    using CustomMapper;
    using EmployeesMapping.App.DTO;
    using EmployeesMapping.Data;

    public class EntryPoint
    {
        public static void Main()
        {
            using (EmployeesMappingContext db = new EmployeesMappingContext())
            {
                var employees = db.Employees
                    .ToList();

                var mapper = new Mapper();

                var mapped = employees
                    .Select(x => mapper.Map<EmployeeDto>(x))
                    .ToList();
            }
        }
    }
}
