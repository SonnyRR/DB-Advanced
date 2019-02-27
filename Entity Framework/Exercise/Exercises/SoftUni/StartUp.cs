using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators.Internal;

namespace SoftUni
{
    using System.Linq;
    using System.Text;
    using System;
    using SoftUni.Data;
    using SoftUni.Models;

    public class StartUp
    {
        public static void Main()
        {
            // DB Scaffold powershell cmdlet:
            // Scaffold-DbContext -Connection "Server=DESKTOP-R3F6I64\SQLEXPRESS;Database=SoftUni;Integrated Security=True;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data/Models

            using (SoftUniContext context = new SoftUniContext())
            {
                string output = AddNewAddressToEmployee(context);
                Console.WriteLine(output);               
            }
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            context.Employees
                .Select(x => new
                {
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    JobTitle = x.JobTitle,
                    Salary = x.Salary
                })
                .ToList()
                .ForEach(
                    x => builder.AppendLine($"{x.FirstName} {x.LastName} {x.MiddleName} {x.JobTitle} {x.Salary:F2}")
                );

            return builder.ToString();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            context.Employees      
                .Where(x => x.Salary > 50_000)
                .Select(x => new
                {
                    FirstName = x.FirstName,
                    Salary = x.Salary
                })
                .OrderBy(x => x.FirstName)
                .ToList()
                .ForEach(
                    x => builder.AppendLine($"{x.FirstName} - {x.Salary:F2}")
                );

            return builder.ToString();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            context.Employees
                .Where(x => x.Department.Name == "Research and Development")
                .Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DepartmentName = x.Department.Name,
                    Salary = x.Salary
                })
                .OrderBy(x => x.Salary)
                .ThenByDescending(x => x.FirstName)
                .ToList()
                .ForEach(
                    x => builder.AppendLine($"{x.FirstName} {x.LastName} from {x.DepartmentName} - ${x.Salary:F2}")
                );

            return builder.ToString();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            Address addressToAdd = new Address() { AddressText = "Vitoshka 15", TownId = 4};
            context.Addresses.Add(addressToAdd);

            Employee employeeToModify = context.Employees
                .FirstOrDefault(x => x.LastName == "Nakov");

            employeeToModify.Address = addressToAdd;

            context.SaveChanges();

            context.Employees
                .OrderByDescending(x => x.AddressId)
                .Select(x => new {Address = x.Address.AddressText})
                .Take(10)
                .ToList()
                .ForEach(x => builder.AppendLine(x.Address));


        return builder.ToString();
        }
    }
}
