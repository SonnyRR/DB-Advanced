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
                string output = GetLatestProjects(context);
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

        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            context.Addresses
                .Select(x => new
                {
                    AddressText = x.AddressText,
                    CityText = x.Town.Name,
                    EmployeesCount = x.Employees.Count
                })
                .OrderByDescending(x => x.EmployeesCount)
                .ThenBy(x => x.CityText)
                .ThenBy(x => x.AddressText)
                .Take(10)
                .ToList()
                .ForEach(x => builder.AppendLine($"{x.AddressText}, {x.CityText} - {x.EmployeesCount} employees"));

            return builder.ToString();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            var employee = context.Employees
                .Where(x => x.EmployeeId == 147)
                .Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    JobTitle = x.JobTitle,
                    Projects = x.EmployeesProjects.Select(y => new { ProjectName = y.Project.Name }).ToList()
                })
                .FirstOrDefault();

            builder.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var employeeProject in employee.Projects.OrderBy(x => x.ProjectName))
            {
                builder.AppendLine(employeeProject.ProjectName);
            }

            return builder.ToString();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            context.Departments
                .Where(x => x.Employees.Count > 5)
                .OrderBy(x => x.Employees.Count)
                .ThenBy(x => x.Name)
                .Select(x => new
                {
                    DepartmentName = x.Name,
                    ManagerName = $"{x.Manager.FirstName} {x.Manager.LastName}",
                    Employees = x.Employees
                        .Select(y => new
                        {
                            FirstName = y.FirstName,
                            LastName = y.LastName,
                            EmployeeName = $"{y.FirstName} {y.LastName}",
                            Title = y.JobTitle
                        })
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .ToList()
                })

                .ToList()
                .ForEach(x =>
                {
                    builder.AppendLine($"{x.DepartmentName} - {x.ManagerName}");
                    x.Employees.ForEach(y =>
                    {
                        builder.AppendLine($"{y.EmployeeName} - {y.Title}");

                    });
                });

            return builder.ToString();
        }
        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();
            string dateFormat = @"M/d/yyyy h:mm:ss tt";

            context.Projects
                .OrderBy(x => x.Name)
                .ThenByDescending(x => x.StartDate)
                .Select(x => new
                {
                    Name = x.Name,
                    Description = x.Description,
                    StartDate = x.StartDate
                })
                .Take(10)
                .ToList()
                .ForEach(x =>
                {
                    builder.AppendLine(x.Name);
                    builder.AppendLine(x.Description);
                    builder.AppendLine(x.StartDate.ToString(dateFormat, CultureInfo.InvariantCulture));
                });


            return builder.ToString();
        }
    }
}
