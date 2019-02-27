using System.Globalization;
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
                string output = GetEmployeesInPeriod(context);
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

            Address addressToAdd = new Address() { AddressText = "Vitoshka 15", TownId = 4 };
            context.Addresses.Add(addressToAdd);

            Employee employeeToModify = context.Employees
                .FirstOrDefault(x => x.LastName == "Nakov");

            employeeToModify.Address = addressToAdd;

            context.SaveChanges();

            context.Employees
                .OrderByDescending(x => x.AddressId)
                .Select(x => new { Address = x.Address.AddressText })
                .Take(10)
                .ToList()
                .ForEach(x => builder.AppendLine(x.Address));

            return builder.ToString();

        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            string dateFormat = @"M/d/yyyy h:mm:ss tt";

            var employees = context.Employees
                .Where(x => x.EmployeesProjects
                    .Any(y => y.Project.StartDate.Year >= 2001 && y.Project.StartDate.Year <= 2003))
                .Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ManagerFirstName = x.Manager.FirstName,
                    ManagerLastName = x.Manager.LastName,
                    Projects = x.EmployeesProjects
                        .Select(y => new
                        {
                            ProjectName = y.Project.Name,
                            StartDate = y.Project.StartDate,
                            EndDate = y.Project.EndDate
                        })

                })
                .Take(10)
                .ToList();

            foreach (var emp in employees)
            {
                builder.AppendLine(
                    $"{emp.FirstName} {emp.LastName} - Manager: {emp.ManagerFirstName} {emp.ManagerLastName}");

                foreach (var pr in emp.Projects)
                {

                    string endDate = pr.EndDate == null
                        ? "not finished"
                        : ((DateTime)pr.EndDate).ToString(dateFormat, CultureInfo.InvariantCulture);

                    string startDate = pr.StartDate.ToString(dateFormat, CultureInfo.InvariantCulture);

                    builder.AppendLine(
                        $"--{pr.ProjectName} - {startDate} - {endDate}");
                }
            }

            return builder.ToString();
        }
    }
}
